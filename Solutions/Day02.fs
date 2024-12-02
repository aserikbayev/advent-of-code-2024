module Year2024Day02

open System
open System.IO
open System.Linq

type Data() =
    member x.Read() =
        let filename = @"../../../day02_input.txt"

        File.ReadLines(filename)
        |> Seq.map (_.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries))
        |> Seq.map (fun list -> list |> Seq.map Convert.ToInt32)

/// Report is safe if:
/// - Two adjacent levels differ by at least one and at most three (1 <= Abs(pairwise differences) <= 3)
/// - The levels are all strictly increasing or all strictly decreasing (pairwise differences have the same sign)
let isReportSafe (levels: int seq) =
    let pairwiseDifferences = levels |> Seq.pairwise |> Seq.map (fun (a, b) -> b - a)

    let isSafeTransition (difference: int) =
        let x = Math.Abs difference
        1 <= x && x <= 3

    let initialDirection =
        match Seq.tryHead pairwiseDifferences with
        | Some diff when diff > 0 -> 1
        | Some diff when diff < 0 -> -1
        | _ -> 0

    let isMonotonicAndSafe =
        initialDirection <> 0
        && pairwiseDifferences
           |> Seq.forall (fun diff ->
               Math.Sign diff = initialDirection && isSafeTransition diff)

    isMonotonicAndSafe

let part1 =
    let reports = Data().Read()
    reports |> Seq.filter isReportSafe |> Seq.length

printfn $"%A{part1}"

let part2 =
    let reports = Data().Read()

    let isReportSafeWithDampener (report: int seq) =
        let derivedReports = report |> Seq.mapi (fun idx _ -> report |> Seq.removeAt idx)

        [| report |].Concat derivedReports |> Seq.exists isReportSafe

    reports |> Seq.filter isReportSafeWithDampener |> Seq.length

printfn $"%A{part2}"
