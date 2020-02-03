namespace Application
open System

type Eleve = {FirstName:string; LastName:string}


module Eleves = 
    
    let eleves = ["Mickey Mouse", "Gontrand", "Minnie", "Donald"]

    let randomNote = [5.5;3.4]
      //  List.init 10 (fun i -> 
       //     let random = new Random()
       //     random.NextDouble
        //)

    
    let classeMap = Map.empty.Add("324232", randomNote).Add("324132", randomNote)