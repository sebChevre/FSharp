namespace Application.Prompt

open System


module CommonPrompt = 

    let afficheMenu () = 
        printfn "Options possibles:"
        printfn "[q]: quit the application"
        printfn "[l]: liste tous les élèves"
        printfn "[c]: créer nouvel élève"
        printfn "[no]: numéro d'élève voulu]"
        printf ">>> "


