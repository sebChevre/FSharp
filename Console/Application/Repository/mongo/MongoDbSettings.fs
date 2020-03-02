namespace Application.Repository.Mongo

open MongoDB.Bson
open MongoDB.Driver
open MongoDB.Driver


module MongoDbSettings = 

    
    [<Literal>]
    let ConnectionString = "mongodb://127.0.0.1:27017"

    [<Literal>]
    let DbName = "bulletins-notes"

    [<Literal>]
    let EleveCollectionName = "eleves"

    [<Literal>]
    let BulletinEleveCollectionName = "bulletin-eleves"

    let client              = MongoClient(ConnectionString)
    let db                  = client.GetDatabase(DbName)
    let elevesCollection    = db.GetCollection<Dto.Eleve>(EleveCollectionName)
    let bulletinsCollection = db.GetCollection<Dto.BulletinEleve>(BulletinEleveCollectionName)

