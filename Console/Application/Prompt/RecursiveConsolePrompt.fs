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

    let displayEleveDetail () =
        let eleves = getAllEleves
        printfn "%i eleve(s) found" countEleves
        eleves |> List.iter prettyPrintEleve

    let displayEleveForm () = 
        printfn "Nom élève:"
        let nom = Console.ReadLine()
        printfn "Prenom élève:"
        let prenom = Console.ReadLine()
        creerEleve ( nom, prenom )|> ignore
        printfn "Elèves créé"

    let displayMoyennesBranches () =
        let ress = 
            getAllBulletins
                |> List.map (fun bulletin -> bulletin.Notes)
                |> List.concat
                |> List.groupBy (fun res -> res.Branche)
                |> List.map (fun (branche,resultats) -> 
                    let moyenneBranche = 
                        resultats 
                        |> List.map (fun res -> mapDecimal2ToFloat res.Note)
                        |> List.average
                    (branche,moyenneBranche)
                            
                )
              

        printfn "%A" ress

    let traitementChoixOption = fun (inputChoice:string, promptLoop)->
        
        match inputChoice with


        |"l" ->
            displayEleveDetail ()
            //keyToContinue()
            //promptLoop()
        |"m" -> 
            displayMoyennesBranches ()
        |"c" ->
            displayEleveForm ()
        |"b"  ->
            let bulletins = getAllBulletins
            bulletins |> List.iter prettyPrintBulletin
            //keyToContinue()
            //promptLoop()
        | numero ->
            match choixIsInt numero with
            |Some digit -> 
                let eleve = findElevesByNumero digit
                printEleveChoiceResult eleve
                //keyToContinue()
                //promptLoop()        
            |None -> 
                printfn "Only digit to search eleve\n"
                //keyToContinue()
                //promptLoop()
                
        promptLoop()


    
        
    //******* Mode récursif, rapelle de la fonction loop
    let rec mainRecursiveLoop () = 

        afficheMenu()
        let input = Console.ReadLine ()

        match input with
        |"q" -> printfn "Bye!!!"
        |_ -> traitementChoixOption ( input, mainRecursiveLoop )
       
       


   
       

