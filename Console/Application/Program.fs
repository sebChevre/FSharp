// Learn more about F# at http://fsharp.org
namespace Application

open System
open ConsolePrompt

module App = 
(*
    let printNotes (notes:option<List<float>>) =
        match notes with
            |None -> "No notes found"
            |Some s -> s |>  List.fold (fun i d -> i + "" + d)

    let findEleve eleveToFind = 
        printfn "%s" eleveToFind
        match Eleves.classeMap.TryFind eleveToFind with
            |eleve -> eleve
          

          
   

    let notes = List.init 5 (fun i -> 2)


    let getInput () =
        printf "guess:>"
        Console.ReadLine ()

    let output (s:string) =
        printfn "You typed: %s" s

    let rec mainLoop() = 
        let input = getInput()
        output input
        mainLoop()
        *)
    [<EntryPoint>]
    let main argv =
        //printfn "Saisir code élève:"

       // let el = findEleve "324232"

       // Eleves.randomNote

       // printNotes el.Value |> ignore

        //el.Value |> List.map (fun v -> 
        //    printfn "%f" v 
        //)
       
        
        (*
        match Console.ReadLine() with
            | "q" -> ()
            | chaine -> printNotes (findEleve chaine).Value |> ignore
            *)
        //ConsolePrompt.mainRecursiveLoop()
        ConsolePrompt.mainSequenceLoop() |> ignore
        0 // return an integer exit code



