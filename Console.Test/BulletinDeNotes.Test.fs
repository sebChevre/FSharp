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
let `` given a float value out of the range 1 - 6 when this value is used to create a note then the function return None``() =

    let fVal = 34.342
    let dec = decimal2 fVal

    Assert.That (dec, Is.Null)
    Assert.True dec.IsNone

[<Test>]
let `` given a float value equal to 1 when this value is used to create a note then the note is returned``() =
    
    let fVal = float 1
    let dec = decimal2 fVal
    Assert.True dec.IsSome

[<Test>]
let `` given a float value equal to 6 when this value is used to create a note then the note is returned``() =
    
    let fVal = 6.00
    let dec = decimal2 fVal
    Assert.True dec.IsSome

