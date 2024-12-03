module Year2024Day03

open System.IO
open System.Text.RegularExpressions

type Data() =
    member x.Read() =
        let filename = @"../../../day03_input.txt"
        File.ReadLines(filename)

let getMulPairs line =
    let pattern = Regex(@"mul\((\d+),(\d+)\)", RegexOptions.Compiled)

    pattern.Matches(line)
    |> Seq.map (fun m -> (int m.Groups[1].Value, int m.Groups[2].Value))

let part1 =
    let lines = Data().Read()

    lines
    |> Seq.collect getMulPairs
    |> Seq.sumBy (fun (a, b) -> a * b)

printfn $"%A{part1}"

let part2 =
    let lines = Data().Read()

    let fullProgram =
        let joined = String.concat "" lines
        "do()" + joined + "don't()"

    let getExecutableSpans (line: string) =
        let executablePattern = Regex(@"do\(\).+?don't\(\)", RegexOptions.Compiled)
        let executableMemory = executablePattern.Matches line
        executableMemory |> Seq.map (_.Value)

    fullProgram
    |> getExecutableSpans
    |> Seq.collect getMulPairs
    |> Seq.sumBy (fun (a, b) -> a * b)

printfn $"%A{part2}"
