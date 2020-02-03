let printTwoParameters x y = 
   printfn "x=%i y=%i" x y

//explicitly curried version
let printTwoParametersExploded x  =    // only one parameter!
   let subFunction y = 
      printfn "x=%i y=%i" x y  // new function with one param
   subFunction

printTwoParameters 2 3 //retrourn une unit

printTwoParameters 2 //retourne  une fonction

