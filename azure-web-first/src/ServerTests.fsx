#r "../packages/suave/lib/net461/Suave.dll"
#r "../packages/suave.testing/lib/net40/Suave.Testing.dll"
#r "../packages/expecto/lib/net461/Expecto.dll"
#r "../packages/net.http/lib/net46/System.Net.Http.dll"

#load "capserver.fsx"


open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.RequestErrors
open Suave
open Expecto
open Suave.Testing
open Suave.Web
//open Suave.Types
open Suave.Testing

let conf:SuaveConfig = defaultConfig
 
let runWithConfig = runWith (defaultConfig:SuaveConfig)

(*let webPart =  
    choose [  
        path "/" >=> (OK "Home")  
        path "/about" >=> (OK "About")  
    ]  
*)

let wp = Capserver.app


let st = 
        testCase "parsing a large multipart form" <| fun _ ->

      let res =
        runWithConfig wp
        |> req HttpMethod.GET "/" None

      Expect.containsAll res 
            "<title>Capability-based Data Store (external home html)</title>" 
            "Should match title"

runTests Impl.ExpectoConfig.defaultConfig st
