type NomPrimitif = {
    Prenom: string
    Complement:string option
}

let moi = {Prenom="Seb";Complement=None}

printfn "%s %s" moi.Prenom moi.Complement.Value


type Prenom = Prenom of string
type Complement = Complement of string option

type NomComplexe = {
    Prenom: Prenom
    Complement:Complement
}

let liste = ["Seb";"Jacques";"Henry"] |> List.map Prenom

liste |> List.iter (fun prenom ->
    printfn "Prenom: %A" prenom
)

let liste2 = liste |> List.map (fun i -> Prenom ("Inconnu" ))

liste2 |> List.iter (fun prenom ->
    printfn "%A" prenom
)

