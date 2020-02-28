#r @"C:/Users/sce/.nuget/packages/mongodb.bson/2.10.2/lib/net452/MongoDB.Bson.dll"
#r @"C:/Users/sce/.nuget/packages/mongodb.driver/2.10.2/lib/net452/MongoDB.Driver.dll"
#r @"C:/Users/sce/.nuget/packages/mongodb.bson/2.10.2/lib/net452/MongoDB.Bson.dll"
#r @"C:/Users/sce/.nuget/packages/mongodb.driver.core/2.10.2/lib/net452/MongoDB.Driver.Core.dll"
#r @"C:/Users/sce/.nuget/packages/mongodb.libmongocrypt/1.0.0/lib/net452/MongoDB.Libmongocrypt.dll"

#load "D:/FSharp/Console/Application/Domaine/BulletinDeNotes.fs"
#load "D:/FSharp/Console/Application/Repository/MongoDbSettings.fs"


open MongoDB.Bson
open MongoDB.Driver
open Application.Domaine.BulletinDeNotes
open Application.Repository.MongoDbSettings


let private eleves = [
    {Prenom="Mickey"; Nom="Mouse"; Numero=12;}; 
    {Prenom="Gontrand"; Nom=""; Numero=23; };
    {Prenom="Minnie"; Nom=""; Numero=11; };
    {Prenom="Donald"; Nom=""; Numero=10; }
    ]   

testCollection.InsertMany eleves


