﻿namespace Application
open System
open BulletinDeNotes



module Eleves = 

    let eleves = [
        {Prenom="Mickey"; Nom="Mouse"; Numero=12;}; 
        {Prenom="Gontrand"; Nom=""; Numero=23; };
        {Prenom="Minnie"; Nom=""; Numero=11; };
        {Prenom="Donald"; Nom=""; Numero=10; }
        ]   


    //let eleves:Map<int,Eleve> = e4|> Seq.map (fun eleve -> eleve.Numero, eleve) |> Map.ofSeq
    let findElevesByNumero numero = 
        let found = eleves |> List.filter (fun eleve -> eleve.Numero = numero) 
        match found with 
            |[] -> None   
            |[_]-> Some (found.Item 0)
            |_  -> None

            (*
    let notesForEleve eleve =
        let resultats  = DataGenerator.generateRandomResultats 10
        {Eleve=eleve; Notes=resultats}

    let bulletinForEleve eleve = 

        match eleve with
        |Some e -> Some notesForEleve 
        |None -> None
        *)


    

            (*
    let elevesAvecNotes = eleves |> List.map (

    let eleveAvecNote numeroEleve = 
        match 
        *)

