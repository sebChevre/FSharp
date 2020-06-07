#r @"/Users/seb/.nuget/packages/fsharp.data/3.3.3/lib/net45/FSharp.Data.dll"

open FSharp.Data

type DetailCas = CsvProvider<"https://raw.githubusercontent.com/openZH/covid_19/master/fallzahlen_kanton_total_csv/COVID19_Fallzahlen_Kanton_JU_total.csv">


let datas = DetailCas.Load("https://raw.githubusercontent.com/openZH/covid_19/master/fallzahlen_kanton_total_csv/COVID19_Fallzahlen_Kanton_JU_total.csv")

let ligne1 = datas.Rows |> Seq.head

let date = ligne1.Date


for row in datas.Rows do
    printfn "%A" row.Ncumul_conf


printfn "%A" date