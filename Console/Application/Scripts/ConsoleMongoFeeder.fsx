#r @"/Users/seb/.nuget/packages/mongodb.bson/2.10.2/lib/net452/MongoDB.Bson.dll"
#r @"/Users/seb/.nuget/packages/mongodb.driver/2.10.2/lib/net452/MongoDB.Driver.dll"
#r @"/Users/seb/.nuget/packages/mongodb.bson/2.10.2/lib/net452/MongoDB.Bson.dll"
#r @"/Users/seb/.nuget/packages/mongodb.driver.core/2.10.2/lib/net452/MongoDB.Driver.Core.dll"
#r @"/Users/seb/.nuget/packages/mongodb.libmongocrypt/1.0.0/lib/net452/MongoDB.Libmongocrypt.dll"

#load "/Users/seb/Developpement/F#SharpLab/FSharpLab/Console/Application/Domaine/BulletinDeNotes.fs"
#load "/Users/seb/Developpement/F#SharpLab/FSharpLab/Console/Application/Repository/Mongo/Dto.fs"
#load "/Users/seb/Developpement/F#SharpLab/FSharpLab/Console/Application/Repository/Mongo/MongoDbSettings.fs"
#load "/Users/seb/Developpement/F#SharpLab/FSharpLab/Console/Application/Repository/ElevesRepository.fs"

#load "/Users/seb/Developpement/F#SharpLab/FSharpLab/Console/Application/Repository/DataGenerator.fs"
#load "/Users/seb/Developpement/F#SharpLab/FSharpLab/Console/Application/Repository/BulletinDeNotesRepository.fs"
//#r @"C:/Users/sce/.nuget/packages/mongodb.bson/2.10.2/lib/net452/MongoDB.Bson.dll"
//#r @"C:/Users/sce/.nuget/packages/mongodb.driver/2.10.2/lib/net452/MongoDB.Driver.dll"
//#r @"C:/Users/sce/.nuget/packages/mongodb.bson/2.10.2/lib/net452/MongoDB.Bson.dll"
//#r @"C:/Users/sce/.nuget/packages/mongodb.driver.core/2.10.2/lib/net452/MongoDB.Driver.Core.dll"
//#r @"C:/Users/sce/.nuget/packages/mongodb.libmongocrypt/1.0.0/lib/net452/MongoDB.Libmongocrypt.dll"

//#load "D:/FSharp/Console/Application/Domaine/BulletinDeNotes.fs"
//#load "D:/FSharp/Console/Application/Repository/MongoDbSettings.fs"


open MongoDB.Bson
open MongoDB.Driver
open Application.Domaine.BulletinDeNotes
open Application.Repository.Mongo.MongoDbSettings
open Application.Repository

open Application.Repository.Mongo

elevesCollection.DeleteMany(Builders.Filter.Empty)
bulletinsCollection.DeleteMany(Builders.Filter.Empty)

let private elevesInitiaux = [
        ElevesRepository.creerEleve ("Mickey","Mouse"); 
        ElevesRepository.creerEleve ("Gontrand","Arthur");
        ElevesRepository.creerEleve ("Minnie","Mouse");
        ElevesRepository.creerEleve ("Donald","Duck");
    ]   

elevesInitiaux |> List.iter (fun eleve -> printfn "Eleve créé %A" eleve)

//let result = elevesCollection.DeleteMany(Builders.Filter.Empty)

//ElevesRepository.insertManyEleves elevesInitiaux
//elevesCollection.InsertMany elevesInitiaux
let bulletins = 
    ElevesRepository.getAllEleves 
    |> List.map (fun e -> 
        DataGenerator.generateBulletinForEleve e.Numero (DataGenerator.generateRandomResultats 10) 
    )


bulletins 
    |> List.iter (fun bul -> 
        let dtos = Dto.BulletinEleve.fromDomain (bul)
        BulletinDeNotesRepository.createBulletin (dtos)
    )

(*
let es = elevesCollection.Find(Builders.Filter.Empty).ToEnumerable() 

es |> Seq.toList
   |> Seq.iter (fun eleve -> printfn "%A" eleve)

   *)
//bulletinsCollection.Find(Builders.Filter.Empty).ToEnumerable()


        //|> Seq.toList
        //|> Seq.iter (fun eleve -> printfn "%A" eleve)