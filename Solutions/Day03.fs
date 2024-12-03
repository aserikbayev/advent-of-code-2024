module Year2024Day03

open System.IO
open System.Text.RegularExpressions

type Data() =
    member x.Read() =
        let filename = @"../../../day03_input.txt"
        File.ReadLines(filename) |> seq

let part1 =
    let lines = Data().Read()

    let pattern = Regex(@"mul\((\d+),(\d+)\)", RegexOptions.Compiled)

    let matches =
        lines
        |> Seq.map (fun line ->
            pattern.Matches(line)
            |> Seq.sumBy (fun m -> int m.Groups[1].Value * int m.Groups[2].Value)
            )

    Seq.sum matches

printfn $"%A{part1}"

let part2 =
    "TODO"

printfn $"%A{part2}"
