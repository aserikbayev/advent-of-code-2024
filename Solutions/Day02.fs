module Year2024Day02

open System
open System.IO
open System.Linq

type Data() =
    member x.Read() =
        let filename = @"../../../day02_input.txt"

        File.ReadLines(filename)
        |> Seq.map (fun line ->
            line.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries)
            |> Seq.map Convert.ToInt32)

// Report is safe if:
// two adjacent levels differ by at least one and at most three
//   i.e., 1 <= Abs(paiwise differences) <= 3
// the levels are all increasing or all decreasing
//  i.e, pairwise differences are strictly monotonic
//   Math.Sign(pairwise difference) are all the same
//  (diff = 0 => unsafe)
let analyzeReport (levels: int seq) =
    let pairwiseDifferences = levels |> Seq.pairwise |> Seq.map (fun (a, b) -> a - b)

    let isSafeTransition (difference: int) =
        let x = Math.Abs difference
        1 <= x && x <= 3

    let increasing = pairwiseDifferences |> Seq.forall (fun x -> (Math.Sign x = 1))
    let decreasing = pairwiseDifferences |> Seq.forall (fun x -> (Math.Sign x = -1))

    let levelChangesWithinAllowedRange =
        pairwiseDifferences |> Seq.forall isSafeTransition

    (increasing || decreasing) && levelChangesWithinAllowedRange

let part1 =
    let reports = Data().Read()
    reports |> Seq.map analyzeReport |> Seq.filter id |> Seq.length

printfn $"%A{part1}"

let part2 =
    let reports = Data().Read()

    let analyzeReportWithDampening (report: int seq) =
        // I tried to analyse without generating a bunch of sequences in memory
        // but ultimately couldn't write a correct solution.
        // Approach below is not very efficient but it gets the job done
        // 
        // Generate reports with one omitted level
        let derivedReports =
            report |> Seq.indexed |> Seq.map (fun (idx, _) -> report |> Seq.removeAt idx)

        [| report |].Concat derivedReports
        |> Seq.map analyzeReport
        |> Seq.exists id

    reports |> Seq.map analyzeReportWithDampening |> Seq.filter id |> Seq.length

printfn $"%A{part2}"
