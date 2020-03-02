namespace Application.Repository.Mongo

open Application.Domaine
open Application.Domaine.BulletinDeNotes

open MongoDB.Bson
module Dto =

    type Eleve = {
        _id: ObjectId
        Prenom: string
        Nom:    string
        Numero: int

    }

    type Resultat = {
        Note: float
        Branche: string
    }

    type BulletinEleve = {
            _id: ObjectId
            Eleve: int
            Resultats: ResizeArray<Resultat>
    }

    module Eleve =

        let mapNumeroEleveToInt (numero:BulletinDeNotes.NumeroEleve) = 
            let (BulletinDeNotes.NumeroEleve numero') = numero
            numero'

        let fromDomain (eleve:BulletinDeNotes.Eleve) : Eleve =
            let numero = eleve.Numero
            let nom = eleve.Nom
            let prenom = eleve.Prenom
            {_id=ObjectId.GenerateNewId();Prenom=prenom; Nom=nom; Numero=mapNumeroEleveToInt numero}

        let toDomain (eleveDto:Eleve) : BulletinDeNotes.Eleve =
            {Prenom=eleveDto.Prenom;Nom=eleveDto.Nom;Numero=BulletinDeNotes.NumeroEleve eleveDto.Numero}

    module BulletinEleve = 

        //let mapNomBrancheToStr (nomBranche:Branche) = 
        //    let (Branche nomBranche') = nomBranche
        //    nomBranche'

        let fromDomain (bulletin:BulletinDeNotes.BulletinEleve) : BulletinEleve =
            let eleve = Eleve.mapNumeroEleveToInt bulletin.Eleve

            let tr = 
                bulletin.Notes 
                    |> List.map (fun resultat -> 
                        let note = BulletinDeNotes.mapDecimal2ToFloat resultat.Note
                        let branche = resultat.Branche.Nom.ToString ()
                        {Note=note;Branche=branche}
                )

            let a = new ResizeArray<Resultat>()

            tr |> List.iter (fun res -> a.Add res)

            {_id=ObjectId.GenerateNewId();Eleve=eleve; Resultats= a}


        let toDomain (bulletinDto:BulletinEleve) : BulletinDeNotes.BulletinEleve =

            let eleve = BulletinDeNotes.NumeroEleve bulletinDto.Eleve

            let resutats:(BulletinDeNotes.Resultat list) = 
                let arr = bulletinDto.Resultats
                let re = Seq.toList arr

                re 
                |> List.map(fun resultatDto -> 
                    let note = ( BulletinDeNotes.decimal2 resultatDto.Note ).Value
                    let branche = BulletinDeNotes.mapStringToBranche resultatDto.Branche
                    {Note=note;Branche=branche}
                   )
                

            {Eleve=eleve;Notes=resutats}


    