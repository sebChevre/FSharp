module UnitTest1

open NUnit.Framework
open Application
open Application.BulletinDeNotes
open System
open DataGenerator

[<SetUp>]
let Setup() = ()

let genereate100Branches = 
   List.init 100 DataGenerator.pickRandomBranche 

let generate100Resultat  = 
   List.init 100 DataGenerator.randomResultat

let checkResultat resultat =
    Assert.That(mapDecimal2ToFloat resultat.Note,Is.GreaterThanOrEqualTo(1.00))
    Assert.That(mapDecimal2ToFloat resultat.Note,Is.LessThanOrEqualTo(6.00))

    match resultat.Branche with
    |b -> Assert.True (true) 

let filterResultatFloatByBranche nomBranche (branche,resultats)   = 
    branche.Nom.Equals nomBranche

let filterAllemandResultatFloat = filterResultatFloatByBranche NomBranche.Allemand

[<Test>]
let ``given the list of eleves wheng geting the list then the size musst match``() = 

    let expected = 4 //nbre eleves
    let actual = Eleves.eleves.Length
    Assert.That(expected,Is.EqualTo(actual))



[<Test>]
let ``given 100 calls to pickRandomBranche then at least one of each branche musst be generated`` () = 

    let branches = genereate100Branches

    let branchesCount = branches 
                            |> List.countBy (fun branche -> branche)


    Assert.That(branchesCount.Length,Is.EqualTo(3))



[<Test>]
let ``given 100 call to randomResultat then i get randomResultats``() =

    let t = generate100Resultat 
    
    t |> List.iter checkResultat

    //t |> List.iter checkRsultat 

    Assert.True(t.Length.Equals(100))


[<Test>]
let ``given call to random bulletinNote then bulletin notes is ok`` () = 

    let eleve = generateBulletinForEleve (Eleves.findElevesByNumero 12).Value

    let bulletin = eleve (List.init 10 randomResultat)

    printfn "%A" bulletin


[<Test>]
let ``given a bulletin when call moyenne from branche then i get a list groupb by branch``() =

    let eleve = generateBulletinForEleve (Eleves.findElevesByNumero 12).Value

    let bulletin = eleve (List.init 10 randomResultat)

    printfn "notes : %A" bulletin.Notes



    //(fun resultat -> resultat.Branche.Nom.Equals(NomBranche.Allemand))

    let moyennes = moyenneBranches bulletin.Notes

    let moyenneAllemandExpected = bulletin.Notes 
                                |> List.filter filterAllemandResultat
                                |> List.map mapResultatTofLoat
                                |> List.average

    let moyenneAllemand = 
        moyennes |> List.filter filterAllemandResultatFloat
                 |> List.map (fun (branche,note) -> note)  
                 |> List.average
                   

    Assert.That (moyennes.Length, Is.EqualTo 3 )
    Assert.That (moyenneAllemandExpected, Is.EqualTo moyenneAllemand) 

    printfn "moyennes : %A" moyennes

    
(*
[<Test>]
let ``given a call to generate random Resultat then i get randomBranche``() =
   // let t = [1..100] |> List.map (fun iter ->
   //     DataGenerator.generateRandomResultats 10
   // )

    let t = DataGenerator.generateRandomResultats 10

    t |> List.iter checkResultat
    Assert.True(t.Length.Equals(100))
    *)
(*
[<Test>]
let ``given a 2 digit formatted value musst be beetween 1 and 6``() =
    let listeRandom  = DataGenerator.randomNotes 100
    listeRandom |> List.iter (fun e -> 

        //printfn "%A" e
        //let f:double = double e
        Assert.That(e,Is.GreaterThanOrEqualTo(1.00))
        Assert.That(e,Is.LessThanOrEqualTo(6.00))
    )

    
[<Test>]
let ``given a the list of eleves when getting eleve by numero``() =
    let numeroWithEleve = 11
    let eleve = Eleves.findByNumero numeroWithEleve

    Assert.True ( eleve.IsSome )
    Assert.That(eleve.Value.Numero,Is.EqualTo(numeroWithEleve))


[<Test>]
let ``given a the list of eleves when getting eleve with false by numero``() =
    let numeroWithEleve = 123
    let eleve = Eleves.findByNumero numeroWithEleve

    Assert.True(eleve.IsNone)
    *)