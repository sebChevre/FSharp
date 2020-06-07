namespace Application

open System
open System.Collections.Generic
open System.IO
open System.Linq

open System.Threading.Tasks
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open System.IO
open Suave
open Suave.WebPart
open Newtonsoft.Json
open Suave.Filters
open Suave.Operators
open Suave.Successful
open FSharp.Data
open Application.Domaine.CoronaDomaine

module Program =
    
    
    let [<Literal>] donneeCsvJuUrl = "https://raw.githubusercontent.com/openZH/covid_19/master/fallzahlen_kanton_total_csv/COVID19_Fallzahlen_Kanton_JU_total.csv"

    type DetailCas = CsvProvider<donneeCsvJuUrl>

    let datas = DetailCas.Load(donneeCsvJuUrl)

    let ligne = datas.Rows |> Seq.head

    //ligne.Ncumul_ICU

    let heure (time:Option<TimeSpan>)= 
        match time with
        |Some time -> time.ToString(@"hh\:mm"); 
        |None -> ""

    let dates = datas.Rows |> Seq.map (fun x -> 
            printfn "Ces %s" x.Abbreviation_canton_and_fl
            printfn "Ces %s" x.Ncumul_tested
            {   Date=x.Date.ToString(@"dd-MM-yyyy"); 
                Heure=heure x.Time;
                CasConfirmes=x.Ncumul_conf; 
                Hospitalise=x.Ncumul_hosp; 
                SoinsIntensifs=x.Ncumul_ICU; 
                Deces=x.Ncumul_deceased;
                Guerison=x.Ncumul_released
            } 
        )

    let j = JsonConvert.SerializeObject dates 


    printfn "%A" j


    let app : WebPart =
      choose [
        GET >=> path "/" >=> Files.file "public/index.html"
        GET >=> path "/test" >=> 
            Writers.setHeader "Content-Type" "application/json; charset=UTF-8" >=>
            OK ( j )
        GET >=> Files.browseHome
        RequestErrors.NOT_FOUND "Page not found." 
      ]

    let config =
      { defaultConfig with homeFolder = Some (Path.GetFullPath "./public") }


    

    [<EntryPoint>]
    let main args =
        printfn "%s" (Path.GetFullPath "./public")
        printfn "%s" config.homeFolder.Value

        let r = Http.RequestString
                  ( "http://httpbin.org/post", 
                    headers = [ ContentType HttpContentTypes.Json ],
                    body = TextRequest """ {"test": 42} """)

        startWebServer defaultConfig app
        0
