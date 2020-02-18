namespace Application.Repository

open Application.Repository.ElevesRepository
open Application.Repository.DataGenerator

module BulletinDeNotesRepository =

    let bulletins = 
        getAllEleves |> List.map (fun e -> 
            generateBulletinForEleve e (generateRandomResultats 10) 
        )

    let getAllBulletins = bulletins