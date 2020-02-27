namespace Application.Repository

open MongoDB.Bson
open MongoDB.Driver
open Application.Domaine.BulletinDeNotes

module MongoDbSettings = 

    


    [<Literal>]
    let ConnectionString = "mongodb://localhost:27017"

    [<Literal>]
    let DbName = "bulletins-notes"

    [<Literal>]
    let CollectionName = "eleves"
    
    let client         = MongoClient(ConnectionString)
    let db             = client.GetDatabase(DbName)
    let testCollection = db.GetCollection<Eleve>(CollectionName)