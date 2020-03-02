namespace Application.Repository

open Application.Repository
open Application.Repository.Mongo.MongoDbSettings
open Application.Repository.Mongo
open MongoDB.Driver


module BulletinDeNotesRepository =

    let bulletins = 
        ElevesRepository.getAllEleves |> List.map (fun e -> 
            DataGenerator.generateBulletinForEleve e.Numero (DataGenerator.generateRandomResultats 10) 
        )


    let createBulletin bulletin = 
        bulletinsCollection.InsertOne bulletin

    let getAllBulletins = 
        bulletinsCollection.FindSync(Builders.Filter.Empty).ToEnumerable()
        |> Seq.toList
        |> List.map (fun bulletinDto -> (Dto.BulletinEleve.toDomain bulletinDto)) 