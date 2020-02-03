// Learn more about F# at http://fsharp.org
namespace Application

open System


module App = 

    let printNotes notes =
        notes |> List.map (fun i -> printfn "%f" i)

    let findEleve eleveToFind = 
        printfn "%s" eleveToFind
        match Eleves.classeMap.TryFind eleveToFind with
            |eleve -> eleve


   

    let notes = List.init 5 (fun i -> 2)


    [<EntryPoint>]
    let main argv =
        printfn "Hello World from F#!"

        let el = findEleve "324232"

       // Eleves.randomNote

        printNotes el.Value |> ignore

        //el.Value |> List.map (fun v -> 
        //    printfn "%f" v 
        //)

        match Console.ReadLine() with
            | "q" -> ()
            | chaine -> printNotes (findEleve chaine).Value |> ignore

        0 // return an integer exit code



