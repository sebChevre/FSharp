namespace Application.Domaine

open System

module CoronaDomaine = 

    type Ligne = {
        Date:string; 
        Heure:string; 
        CasConfirmes:int; 
        Hospitalise:Nullable<int>; 
        SoinsIntensifs: Nullable<int>; 
        Deces:string; Guerison:string }
