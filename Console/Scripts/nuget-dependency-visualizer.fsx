#r @"C:/Users/sce/.nuget/packages/streams/0.5.0/lib/net45/Streams.dll"


open System
open System.IO
open System.Collections.Generic
open Nessos.Streams

// make Visual Studio use the script directory
Directory.SetCurrentDirectory(__SOURCE_DIRECTORY__)

type PackageType =
    | InScopeOfAnalysis // lightgreen
    | WeDependOnPackage // grey
    | PackageDependOnUs // lightblue

type DependencySet =
    { dependent: string;
      packageType: PackageType
      dependencies: string Set}

// ================================
// Generate the graph using GraphViz
// ================================

module GraphViz =

    // change this as needed for your local environment
    let graphVizPath = @"d:\Program Files (x86)\graphviz-2.38\bin\"

    let getName (n) =
        sprintf "\"%s\"" n     // be sure to quote the type name!

    let toCsv sep strList =
        match strList with
        | [] -> ""
        | _ -> List.reduce (fun s1 s2 -> s1 + sep + s2) strList

    let writeDepSet writer depSet =
        let fromNode = getName depSet.dependent
        let toNodes =
            depSet.dependencies
            |> Seq.map getName
            |> Seq.sort  // make it more human readable
            |> Seq.toList
            |> toCsv "; "
        fprintfn writer "   %s -> { rank=none; %s }" fromNode toNodes

    // Create a DOT file for graphviz to read.
    let createDotFile dotFilename depSets =
        use writer = new System.IO.StreamWriter(path=dotFilename)
        fprintfn writer "digraph G {"
        fprintfn writer "    page=\"40,60\"; "
        fprintfn writer "    ratio=auto;"
        fprintfn writer "    rankdir=LR;"
        fprintfn writer "    fontsize=10;"
        // Write edges
        depSets
        |> Seq.sort   // make it more human readable
        |> Seq.iter (writeDepSet writer)
        // Write color information
        depSets
        |> Seq.iter (fun depSet->
            let fromNode = getName depSet.dependent
            let color =
                match depSet.packageType with
                | InScopeOfAnalysis -> "green" //color=".7 .3 1.0"]
                | PackageDependOnUs -> "lightblue"
                | WeDependOnPackage -> "grey"
            fprintfn writer "   %s [color=%s,style=filled];" fromNode color
        )
        fprintfn writer "   }"

    // shell out to run a command line program
    let startProcessAndCaptureOutput cmd cmdParams =
        let debug = false

        if debug then
            printfn "Process: %s %s" cmd cmdParams
        let si = new System.Diagnostics.ProcessStartInfo(cmd, cmdParams)
        si.UseShellExecute <- false
        si.RedirectStandardOutput <- true
        use p = new System.Diagnostics.Process()
        p.StartInfo <- si
        if p.Start() then
            if debug then
                use stdOut = p.StandardOutput
                stdOut.ReadToEnd() |> printfn "%s"
                printfn "Process complete"
        else
            printfn "Process failed"

    /// Generate an image file from a DOT file
    /// algo = dot, neato
    /// format = gif, png, jpg, svg
    let generateImageFile dotFilename algo format imgFilename =
        let cmd = sprintf @"""%s%s.exe""" graphVizPath algo
        let inFile = System.IO.Path.Combine(__SOURCE_DIRECTORY__,dotFilename)
        let outFile = System.IO.Path.Combine(__SOURCE_DIRECTORY__,imgFilename)
        let cmdParams = sprintf "-T%s -o\"%s\" \"%s\"" format outFile inFile 
        startProcessAndCaptureOutput cmd cmdParams

// ================================
// NuGet packages analysis
// ================================

#I @"C:/Users/sce/.nuget/packages/nuget.core/2.14.0/lib/net40-Client"
#r "NuGet.Core.dll"
#r "System.Xml.Linq.dll"

let repository =
    NuGet.PackageRepositoryFactory.Default.CreateRepository
        "https://nuget.org/api/v2"

// Download info about all NuGet packages
let allNuGetPackages =
    repository.GetPackages()
    |> Stream.ofSeq
    |> Stream.filter (fun p ->
        // I need this to see the progress of download
        printfn "%s" p.Id
        true
    )

// Select only latest versions to analyze
// If you need more accurate analysis you should not avoid versioning
let latestVersionOfNuGetPackages =
    allNuGetPackages
    |> Stream.groupBy (fun p -> p.Id)
    |> Stream.map (fun (key, packages) ->
        packages
        |> Stream.ofSeq
        |> Stream.filter (fun x-> x.Published.HasValue)
        |> Stream.maxBy (fun x->x.Published.Value))

// Build index based on package.Id
let packages =
    latestVersionOfNuGetPackages
    |> Stream.map (fun x->x.Id.ToLowerInvariant(), x)
    |> Stream.toSeq
    |> Map.ofSeq

// Print graph into file
let printGraph name (selectedPackageIds:Dictionary<_,_>) =
    let depSet =
        latestVersionOfNuGetPackages
        |> Stream.filter (fun p->selectedPackageIds.ContainsKey(p.Id.ToLowerInvariant()))
        |> Stream.map (fun p ->
            {dependent = p.Id;
             packageType = selectedPackageIds.[p.Id.ToLowerInvariant()];
             dependencies =
                seq {
                    for set in p.DependencySets do
                        for dep in set.Dependencies do
                            if selectedPackageIds.ContainsKey (dep.Id.ToLowerInvariant())
                               && packages.ContainsKey(dep.Id.ToLowerInvariant())
                                then yield packages.[dep.Id.ToLowerInvariant()].Id
                }|> Set.ofSeq})
    // create DOT file
    let dotFilename = name+ ".dot"
    GraphViz.createDotFile dotFilename (Stream.toSeq depSet)

    // create SVG file
    let svgFilename = dotFilename + ".svg"
    GraphViz.generateImageFile dotFilename "dot" "svg" svgFilename
    //GraphViz.generateImageFile dotFilename "dot" "png" (dotFilename + ".png")

// Create dependency graph based on initial `selector`
let createGraph name selector =
    // Ids of packages that will be displayed on graph
    let selectedPackageIds = Dictionary<string, PackageType>()

    // Mark package with all dependant packages
    let rec markPackage (id:string) mark =
       let key = id.ToLowerInvariant()
       if not <| selectedPackageIds.ContainsKey key then
            selectedPackageIds.Add(key, mark) |> ignore
            if packages.ContainsKey key then
                let package = packages.[key]
                for set in package.DependencySets do
                    for dep in set.Dependencies do
                        markPackage dep.Id WeDependOnPackage
            else
                printfn "Reference to unlisted package '%s'" key
       else
        if (mark = InScopeOfAnalysis && selectedPackageIds.[key] <> mark)
            then selectedPackageIds.[key] <- mark

    // Find and mark of F# packages
    latestVersionOfNuGetPackages
    |> Stream.filter selector
    |> Stream.iter (fun p->
        printfn "Base package: %s" p.Id
        markPackage p.Id InScopeOfAnalysis)

    // Check if package has marked dependant package
    let isDependOnMarkedPackage (package:NuGet.IPackage) =
        seq {
            for set in package.DependencySets do
                for dep in set.Dependencies do
                    yield dep.Id.ToLowerInvariant()
        }
        |> Seq.exists (fun id->
            match selectedPackageIds.TryGetValue id with
            | true, InScopeOfAnalysis
            | true, PackageDependOnUs
                -> true
            | _ -> false
           )

    // Find all packages that depend on marked/F# packages
    let state = ref true
    while !state do
        state := false
        for p in Stream.toSeq latestVersionOfNuGetPackages do
            let key = p.Id.ToLowerInvariant()
            if not (selectedPackageIds.ContainsKey(key)) &&
               isDependOnMarkedPackage p
               then state := true
                    printfn "\tDependent package: %s" key
                    selectedPackageIds.Add(key, PackageDependOnUs)

    printGraph name selectedPackageIds

// ================================
// Samples
// ================================

let isFSharpPackage (p:NuGet.IPackage) =
    let s = String.Join(":", [p.Title; p.Tags; p.Id; p.Description]).ToLowerInvariant()
    (s.Contains "fsharp" || s.Contains "f#")
        && not(s.Contains "pdfsharp")
        && not(s.Contains "rdfsharp")
        && not(s.Contains "funscript") // too much dependencies

createGraph "FSharp.Ecosystem" isFSharpPackage


createGraph "FSharp.Compiler.Service" (fun p-> p.Id = "FSharp.Compiler.Service")

createGraph "FsPickler" (fun p-> p.Id = "FsPickler")
createGraph "FSharp.Data" (fun p-> p.Id.Contains("FSharp.Data"))

createGraph "FSharp.Core" (fun p-> p.Id.StartsWith("FSharp.Core"))
createGraph "FSharpx" (fun p-> p.Id.Contains("FSharpx"))

createGraph "Roslyn" (fun p-> p.Id.Contains("Roslyn"))
