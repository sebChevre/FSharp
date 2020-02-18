namespace Application.Repository

open System
open Application.Domaine.BulletinDeNotes

module DataGenerator = 
    
    let rnd = System.Random()

    let multiplyBy5 = ( * ) 5.00

    let add1 = ( + ) 1.00

    let applyFormat decimalValue =
        let v = decimal2 (Math.Round(decimalValue |> multiplyBy5 |> add1,2))
        match v with
        |Some s -> s
        |None -> failwith "Failed"
        
    let pickRandomBranche i =
        let alea = rnd.Next 3

        printfn "random %i" alea

        match alea with
        |0 -> {Nom=NomBranche.Allemand}
        |1 -> {Nom=NomBranche.Francais}
        |2 -> {Nom=NomBranche.Math}
        |_ -> {Nom=NomBranche.Francais}


    
    //genere un resultat random pour une branche random
    let randomResultat i = 
        let rawRandom = rnd.NextDouble ()
        let branche = pickRandomBranche 1
        {Note=applyFormat rawRandom ;Branche=branche}


    let generateBulletinForEleve eleve resultat = 
        //random resulats
        {Eleve=eleve;Notes=resultat}


    let generateRandomResultats count =
        List.init count (fun _ -> randomResultat ())

 

