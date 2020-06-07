#r @"/Users/seb/.nuget/packages/suave/2.5.6/lib/net461/Suave.dll"


open Suave

startWebServer defaultConfig (Successful.OK "Hello World!")