
type Eleve = {Id:string; Notes:List<float>}


let notes = [3.5;2.3;4.5;5.1;3.4]
let e = {Id="322313";Notes=notes}

let eleveMap = Map.empty.Add("1", e).Add("2", e)

let choix = "1"

let eleve = match eleveMap.TryFind choix with
            |ele -> ele
           

match eleve with
|Some eleve -> 
    printfn "Eleve found: %s" eleve.Id
    eleve.Notes |> List.map (fun i -> printfn "Note: %f" i) |> ignore
    eleve.Notes |> List.average |> printfn "Moyenne: %f"
    eleve.Notes |> List.length |> printfn "Nbre notes: %i"
|None -> 
    printfn "No eleve found" 
    

//printfn "%s" eleve.Id