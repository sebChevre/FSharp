namespace Application.Prompt

open System
open Application.Repository
open Application.Prompt.CommonPrompt

module SequenceConsolePrompt =

    let writeResponse s = 
        match s with
            |"q" -> 
                printfn "Bye!!!"
            |"l" ->
                printfn "All eleves...to be implemented"
            | numero ->
                printfn "Choix %s" numero

                let eleve = ElevesRepository.findElevesByNumero (numero |> int)

                match eleve with
                |Some e -> printfn "%A" e
                |None -> printfn "no eleve found with this numero"

    let getLine = fun _->

        Console.ReadLine ()

    let rec mainSequenceLoop () = 
        printfn "end loop"
        let lignes = Seq.initInfinite getLine
        Seq.iter writeResponse (lignes)

