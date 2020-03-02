let square c = c * c

// avec pipe
printfn "ok %i" <|square 4
//parenthese
printfn "ok %i" (square 4)

let cinq = 5
printfn "cinq %i" 5

let six () = 6 
printfn "six %i" <|six()

//un instruction en console en lançant le fichier ????, dernier réultat
let if' b t f = if b then t else f
let false'  = if' false "true" "false"
let true' = if' true "true" "false"

let intToString x = sprintf "x is %i" x  // format int to string
let stringToInt x = System.Int32.Parse(x)

//fonctions en paramètres
let evalWith5ThenAdd2 fn = fn 5 + 2     // same as fn(5) + 2
evalWith5ThenAdd2 

let double c = 2 * c
evalWith5ThenAdd2 double 

//fonctions en sortie
let adderGenerator numberToAdd = (+) numberToAdd
let add5 = adderGenerator 5
add5 12

//tuples
let tup  = (1,"test")

let genericTupleFn aTuple = 
   let (x,y) = aTuple
   printfn "x is %A and y is %A" x y

let tupleToConcat tuple = 
    let (a,b) = tuple
    printfn "b: %s" b

tupleToConcat tup

type Test = {
    Test1: string;
    Test2: int;
}

let rnd = System.Random()

let generateRandTest i = 
    {Test1 = "yep";Test2 = 25}

let print i = 
    printfn "print %i" i

let printGen i = 
    printfn "print %A" i

let listTests = [0..100]|> List.map generateRandTest

[0..100]|> List.iter print

listTests |> List.iter printGen

let add1 x = x + 1

let add1IfSomething = Option.map add1

Some 34 |> add1IfSomething

None |> add1IfSomething

let valeurs = [Some 2;Some 3;Some 4;None;Some 2;Some 1]

valeurs |> List.map add1IfSomething 