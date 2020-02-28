#r "C:/Users/sce/.nuget/packages/fsharp.data/3.3.3/lib/net45/FSharp.Data.dll"

open FSharp.Data

let apiUrl = "https://api.openweathermap.org/data/2.5/weather?q=London&appid=22c800fad6d5eace1a868376b8707a6c"
type Weather = JsonProvider<"https://api.openweathermap.org/data/2.5/weather?q=London&appid=22c800fad6d5eace1a868376b8707a6c">
let weathertest = Weather.Wind

type TestLocalJson = JsonProvider<"C:/Users/sce/test.json">
let test = TestLocalJson.Load("C:/Users/sce/test.json")
printfn "%s" test.Test99
