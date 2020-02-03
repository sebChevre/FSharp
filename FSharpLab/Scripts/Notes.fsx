

type Eleve = {id:string; Notes:List<float>}



//******** fonctions *********
let initializeResultList size  =
    let results = List.replicate size 0.0
    results




//****************************

//tableau de notes
let notes = [3.5;2.3;4.5;5.1;3.4]
let e = {id="322313";Notes=notes}

let classeMap = Map.empty.Add("324232", e).Add("324132", e)

//nbre de notes

// recu elelve

match classeMap.TryFind("324232") with
    | Some(notes) -> printfn "nbre de notes: %i" notes.Notes.Length
    | None -> printfn "aucun note trouvé"


Console.ReadLine()
//let classe = mapWithElements


