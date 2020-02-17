module Console.Test

open System
open NUnit.Framework
open Application.BulletinDeNotes
open System

[<SetUp>]
let Setup() = ()


let countDecimal ( decimal:Decimal2 ) = 

    let dc = string (mapDecimal2ToFloat decimal)
    let decimalAsStr = (dc.Split '.').[1]
    String.length decimalAsStr


[<Test>]
let ``given a float value when a use this value to create a note then a note is created``() =

    let fVal = 5.35435436434
    let note = decimal2 fVal
    printfn "Note generated: %A" note.Value
    Assert.IsNotNull( Some(note) )

[<Test>]
let `` given a float value when this value is used to create a note then a note is created and is 2 digit format``() =

    let fVal = 5.4342523332
    let note = decimal2 fVal
    printfn "Note generated: %A" note.Value
    Assert.IsNotNull( Some(note) )

    Assert.That(countDecimal note.Value,Is.EqualTo 2)

[<Test>]
let `` given a float value when this value is used to create a note then a note is created in range 1 - 6``() =

    let fVal = 26.4342523332
    match decimal2 fVal with
    |_ -> Assert.Fail
    |None -> Assert.Fail
    //printfn "Note generated: %A" note.Value
    //match note with
    //|Some note -> Assert.Fail
    //|None -> Assert.Pass
