#r @"/Users/seb/.nuget/packages/fscheck/2.14.2/lib/net452/FsCheck.dll"

#load "capserver.fsx"

open System
open System.Text
open FsCheck
open Suave
open Suave.Http.HttpMethod

//let WithUri u hc =

//    let b = HttpBinding.createSimple Protocol.HTTP "127.0.0.1" 8080

//    let uri = new System.Uri("http://some.random.tld" + u)
   // let rawQuery = uri.Query.TrimStart('?')
//    let req = { hc.request with 
           //     url = uri 
           //     rawQuery = rawQuery }
//    { hc with request = req }

//let AsGetRequest hc =
 //   let req = { hc.request with ``method`` = HttpMethod.GET }
  //  { hc with request = req }


let makeReq method urlPath data =

    let b = HttpBinding.createSimple Protocol.HTTP "127.0.0.1" 8080

    { HttpRequest.empty with
        rawMethod = method.ToString ()
        binding = b
        //url = new Uri(sprintf "http://localhost/%s" urlPath)
        rawForm = data
    }

let makeCreate data =
  makeReq Http.PUT "api/create" data

let makeRead guid =
  makeReq Http.GET (sprintf "api/read/%s" (guid.ToString())) Array.empty

let makeDelegate rud guid =
  makeReq Http.GET
    (sprintf "api/delegate/%s/%s" rud (guid.ToString())) Array.empty

let makeUpdate data guid =
  makeReq Http.POST (sprintf "api/update/%s" (guid.ToString())) data

let makeDelete guid =
  makeReq Http.DELETE (sprintf "api/delete/%s" (guid.ToString())) Array.empty

let sendReq req =
  Capserver.app { HttpContext.empty with request = req }
  |> Async.RunSynchronously
  |> Option.get

let getResponse req =
  let context = sendReq req
  match context.response.content with
  | Bytes x -> x
  | _ -> Array.empty

let getGuid req =
  getResponse req
  |> Encoding.UTF8.GetString
  |> Guid.Parse

let getOk req =
  let context = sendReq req
  context.response.status = {code=200;reason="OK"}//HttpCode.HTTP_200

let getCode req =
  let context = sendReq req
  context.response.status

type Properties =
  static member ``Let's make a bunch of fake docs and leave them on the server`` data =
    (makeCreate data |> getCode) = {code=201;reason="Created"}//HttpCode.HTTP_201

  static member ``Can create and then read`` data =
    let dataResponse =
      makeCreate data
      |> getGuid
      |> makeRead
      |> getResponse
    dataResponse = data

  static member ``Can create and then delete`` data =
    let status =
      makeCreate data
      |> getGuid
      |> makeDelete
      |> getCode
    status = {code=204;reason="No Content"}//HttpCode.HTTP_204

  static member ``Can create and then update and then delete`` data1 data2 =
    let guid = makeCreate data1 |> getGuid
    let status1 = makeUpdate data2 guid |> getCode
    let dataResponse = makeRead guid |> getResponse
    let status2 = (makeDelete guid |> getCode)
    dataResponse = data2
    && status1 = {code=204;reason="No Content"}
    && status2 = {code=204;reason="No Content"}

  static member ``Can't read after delete`` data =
    let guid = makeCreate data |> getGuid
    (makeDelete guid |> getCode) = {code=204;reason="No Content"} &&
    (makeRead guid |> getCode) = {code=400;reason="Bad Request"}//HttpCode.HTTP_400

  static member ``An invented GUID won't have a doc`` (guid: Guid) =
    (makeRead guid |> getCode) = {code=400;reason="Bad Request"}

  static member ``Can create and then delegate and then read`` data =
    let guid1 = makeCreate data |> getGuid
    let guid2 = makeDelegate "r" guid1 |> getGuid
    let dataResponse = makeRead guid2 |> getResponse
    let status1 = (makeDelete guid1 |> getCode)
    let status2 = (makeDelete guid2 |> getCode)
    dataResponse = data
    && status1 = {code=204;reason="No Content"}
    && status2 = {code=204;reason="No Content"}

Check.All (Config.QuickThrowOnFailure, typeof<Properties>)
