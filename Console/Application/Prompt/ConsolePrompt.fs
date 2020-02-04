namespace Application.Prompt

open System

module ConsolePrompt =

    let dealInputPrompt = fun (s, cb)->
        match s with
            |"q" -> 
                printfn "Bye!!!"
            |"l" ->
                printfn "All eleves...to be implemented"
                cb()
            |numero ->
                printfn "Choix %s" numero
                cb()    

  
    
    let printPrompt () = 
        printf "Choix [q: quit, l: liste tous les élèves, [no]; numéro d'élève voulu]:>"
    
    let getInput () =
        printPrompt ()
        Console.ReadLine ()

    let getLine = fun _->
        printPrompt ()
        Console.ReadLine ()

    let writeResponse s = 
        match s with
            |"q" -> 
                printfn "Bye!!!"
            |"l" ->
                printfn "All eleves...to be implemented"
            |numero ->
                printfn "Choix %s" numero
        

    //******* Mode récursif, rapelle de la fonction loop
    let rec mainRecursiveLoop () = 
       let input = getInput()
       
       dealInputPrompt (input,mainRecursiveLoop)
       

       (*
        match input with
            |"q" -> 
                printfn "Bye!!!"
            |"l" ->
                printfn "All eleves...to be implemented"
                mainRecursiveLoop()
            |numero ->
                printfn "Choix %s" numero
                mainRecursiveLoop()
    *)
    //Fin récursif
    
    //mode séqeunce
    let rec mainSequenceLoop () = 
        let lignes = Seq.initInfinite getLine
        Seq.iter writeResponse (lignes)
       


   
            
       

