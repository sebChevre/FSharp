namespace Application.Prompt

open System


open Application.Prompt.CommonPrompt
open Application.Repository.ElevesRepository
open Application.Repository.BulletinDeNotesRepository
open Application.Domaine.BulletinDeNotes


module ConsolePrompt =

    let choixIsInt (inputChoice:string) = 
        match System.Int32.TryParse inputChoice with
            |true, numero -> Some numero
            |false, _ -> None

    let printEleveChoiceResult optEleve = 
        match optEleve with
        |Some eleve -> printfn "%A\n" eleve
        |None -> printfn "no eleve found with this numero\n"  

    let prettyPrintEleve eleve =
        printfn "%A" eleve

    let prettyPrintBulletin (bulletin:BulletinEleve) =
        printfn "%A" bulletin

    let dealInputPrompt = fun (inputChoice:string, promptLoop)->
        
        match inputChoice with
        |"q" -> 
            printfn "Bye!!!"
        |"l" ->
            let eleves = getAllEleves
            eleves |> List.iter prettyPrintEleve
            promptLoop()
        |"b"  ->
            let bulletins = getAllBulletins
            bulletins |> List.iter prettyPrintBulletin
            promptLoop()
        | numero ->
            match choixIsInt numero with
            |Some digit -> 
                let eleve = findElevesByNumero digit
                printEleveChoiceResult eleve
                promptLoop()        
            |None -> 
                printfn "Only digit to search eleve\n"
                promptLoop()


    let getInput () =
        printPrompt ()
        Console.ReadLine ()

    
        
    //******* Mode récursif, rapelle de la fonction loop
    let rec mainRecursiveLoop () = 

      
       let input = getInput()
       
       dealInputPrompt (input,mainRecursiveLoop)
   
       

