namespace Application.Repository

open Application.Domaine.BulletinDeNotes
open Application.Repository.Mongo.MongoDbSettings
open Application.Repository.Mongo
open MongoDB.Driver
open MongoDB.Bson
open System.Collections.Generic

module ElevesRepository = 

    //  UTILS
    let countEleves = 
        elevesCollection.CountDocuments( Builders.Filter.Empty )

    let noExist (no:int)=
        elevesCollection.FindSync(fun eleve -> eleve.Numero = no ).ToEnumerable() 
        |> Seq.toList
        |> List.length > 0

    let rec generateNextNumero () = 
        let start = countEleves;
        let mille = int64(1000 + System.Random().Next 100) 
        let no = int(( + ) start mille)
        match noExist no with
        |true -> generateNextNumero ()
        |false -> NumeroEleve no




    //  CONSTRUCTEURS
    let creerEleve (nom:string, prenom:string) =
        let no = generateNextNumero ()
        let eleve = {Prenom=prenom;Nom=nom;Numero=generateNextNumero()}
        elevesCollection.InsertOne ( Dto.Eleve.fromDomain eleve )
        eleve

    let insertManyEleves ( eleves: Eleve list ) = 
        elevesCollection.InsertMany (eleves |> List.map (fun eleve -> Dto.Eleve.fromDomain eleve) |> List.toSeq)

    let getAllEleves:Eleve list = 
        elevesCollection
            .FindSync(Builders.Filter.Empty).ToEnumerable() 
            |> Seq.toList
            |> List.map (fun eleveDto -> Dto.Eleve.toDomain eleveDto) 
   
    //let eleves:Map<int,Eleve> = e4|> Seq.map (fun eleve -> eleve.Numero, eleve) |> Map.ofSeq
    let findElevesByNumero numero = 
        let found = getAllEleves |> List.filter (fun eleve -> eleve.Numero = NumeroEleve numero) 
        match found with 
            |[] -> None   
            |[_]-> Some (found.Item 0)
            |_  -> None

    //let getAllEleves = eleves
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

