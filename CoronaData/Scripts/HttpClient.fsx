#r @"/Users/seb/.nuget/packages/fsharp.data/3.3.3/lib/net45/FSharp.Data.dll"
open FSharp.Data




let resp = Http.RequestString("http://localhost:8500/v1/agent/services", httpMethod = "GET")

printfn "%A" resp


[<Literal>]
let serviceRegisterDefinition = """
{
  "ID": "corona1",
  "Name": "corona",
  "Tags": [
    "primary",
    "v1"
  ],
  "Address": "127.0.0.1",
  "Port": 8083,
  "Meta": {
    "corona_version": "1.0"
  },
  "EnableTagOverride": false,
  "Check": {
    "DeregisterCriticalServiceAfter": "90m",
    "Http": "localhost:8083",
    "Interval": "10s",
    "Timeout": "5s"
  },
  "Weights": {
    "Passing": 10,
    "Warning": 1
  }
}
"""

let resp2 = Http.Request( "http://localhost:8500/v1/agent/service/register", httpMethod = "PUT", body = TextRequest serviceRegisterDefinition)

printfn "%A" resp2.StatusCode
printfn "%A" resp2.Body