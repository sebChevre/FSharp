#r @"/Users/seb/.nuget/packages/suave/2.5.6/lib/net461/Suave.dll"



open System.IO
open Suave
open Suave.Filters
open Suave.Operators



let app : WebPart =
  choose [
    GET >=> path "/" >=> Files.file "index.html"
    GET >=> Files.browseHome
    RequestErrors.NOT_FOUND "Page not found." 
  ]

let config =
  { defaultConfig with homeFolder = Some (Path.GetFullPath "public") }

//printfn "%s" (Path.GetFullPath "./public")
   
startWebServer config app