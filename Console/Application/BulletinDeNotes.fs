namespace Application

open System



module BulletinDeNotes = 

    let multiplyBy5 = ( * ) 5.00

    let add1 = ( + ) 1.00

    type Decimal2 = private Decimal2 of float

    let ifTrueThen success = function
        | true -> Some success
        | false -> None

    let (|IsNotBeetween1And6|_|) decimalValue =
        printfn "vale: %f" decimalValue
        (decimalValue < float 1 || decimalValue > float 6)
        |> ifTrueThen IsNotBeetween1And6


    let applyFormatDecimal2 (valeur:float) =
        (Math.Round(valeur,2))

    let decimal2 = function 
        | IsNotBeetween1And6 -> None
        | valeur -> applyFormatDecimal2 valeur |> Decimal2 |>Some


        
    type Eleve = {Prenom: string; Nom: string; Numero: int}

    type NomBranche = Francais | Math | Allemand

    type Branche = {Nom: NomBranche;}






    type Resultat = {Note: Decimal2;Branche: Branche;}

    type BulletinEleve = {Eleve: Eleve; Notes: List<Resultat>}

    //***** Création *******
    let createResultat note branche = {Note=note;Branche=branche}

    //***** filtres, map*****
    let filtreResultatByBranche resultat = resultat.Branche

    let mapDecimal2ToFloat (note:Decimal2) = 
        let (Decimal2 note') = note
        note'

    let mapResultatTofLoat (resultat:Resultat) = 
        mapDecimal2ToFloat resultat.Note


    let moyenneNotes (resultats:List<Resultat>) = 
        resultats
            |> List.map 
                mapResultatTofLoat
            |> List.average 

    

    let mapBrancheByMoyenne (branche,resultats:List<Resultat>) =
        let moyenne = 
            resultats
            |> moyenneNotes
        (branche,moyenne)



    let moyenneBranches (resultats:List<Resultat>) = 
        resultats 
        |> List.groupBy filtreResultatByBranche
        |> List.map mapBrancheByMoyenne

    let filterResultatByBranche nomBranche resultat  = 
        resultat.Branche.Nom.Equals nomBranche

    let filterAllemandResultat = filterResultatByBranche NomBranche.Allemand





