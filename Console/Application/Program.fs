// Learn more about F# at http://fsharp.org
namespace Application

open System
open Application.Prompt


module App = 

    [<EntryPoint>]
    let main argv =
       
        ConsolePrompt.mainRecursiveLoop()
        //ConsolePrompt.mainSequenceLoop() |> ignore
        0 // return an integer exit code



