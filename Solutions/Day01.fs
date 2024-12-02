module Year2024Day01

open System
open System.IO

type Data() =
    member x.Read() =
        let lists =
            File.ReadLines(@"../../../day01_input.txt")
            |> Seq.map (_.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries))

        let left, right =
            lists |> Seq.map (fun arr -> Convert.ToInt32 arr[0]), lists |> Seq.map (fun arr -> Convert.ToInt32 arr[1])

        left, right

let getData () =
    let data = Data().Read()
    data

let part1 =
    let left, right = getData ()

    let distance (a: int, b: int) = Math.Abs(b - a)

    let left_sorted = Seq.sort left
    let right_sorted = Seq.sort right

    Seq.zip left_sorted right_sorted
    |> Seq.map (fun (l, r) -> distance (l, r))
    |> Seq.sum


printfn $"%A{part1}"

let part2 =
    let leftList, rightList = getData ()

    let frequencies list =
        list
        |> Seq.groupBy id
        |> Seq.map (fun (key, items) -> (key, Seq.length items))
        |> Map.ofSeq

    let leftFreq = frequencies leftList
    let rightFreq = frequencies rightList

    leftFreq.Keys
    |> Seq.filter rightFreq.ContainsKey
    |> Seq.sumBy (fun key -> key * leftFreq[key] * rightFreq[key])

printfn $"%A{part2}"
