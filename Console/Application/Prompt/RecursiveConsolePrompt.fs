namespace Application.Prompt

open System


open Application.Prompt.CommonPrompt
open Application.Repository.ElevesRepository

module ConsolePrompt =

    let choixIsInt (inputChoice:string) = 
        match System.Int32.TryParse inputChoice with
            |true, numero -> Some numero
            |false, _ -> None

    let dealInputPrompt = fun (inputChoice:string, cb)->
        
        match inputChoice with
        |"q" -> 
            printfn "Bye!!!"
        |"l" ->
            let eleves = getAllEleves
            getAllEleves |> printfn "%A"
            //printfn "All eleves...to be implemented\n"
            cb()
        | numero ->
            match choixIsInt numero with
            |Some digit -> 
                let eleve = findElevesByNumero (digit)
                
                match eleve with
                |Some eleve -> printfn "%A\n" eleve
                |None -> printfn "no eleve found with this numero\n"
                cb()        
            |None -> 
                printfn "Only digit to search eleve\n"
                cb()


    let getInput () =
        printPrompt ()
        Console.ReadLine ()

    
        
    //******* Mode récursif, rapelle de la fonction loop
    let rec mainRecursiveLoop () = 
       let input = getInput()
       
       dealInputPrompt (input,mainRecursiveLoop)
   
       

