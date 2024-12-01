module Year2024Day01

open System
open System.IO
open System.Collections.Generic
open System.Linq

type Data() =
    member x.Read() =
        File.ReadLines(@"../../../input.txt")
        |> Seq.map (_.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries))
        |> Seq.fold
            (fun (leftList: List<int>, rightList: List<int>) arr ->

                if arr.Length = 2 then
                    leftList.Add(Convert.ToInt32 arr[0])
                    rightList.Add(Convert.ToInt32 arr[1])

                (leftList, rightList))
            (List<int>(), List<int>())

let getData () =
    let data = Data().Read()
    data

let part1 =
    let left, right = getData ()

    let left_sorted = Seq.sort left
    let right_sorted = Seq.sort right

    let pairs = Seq.zip left_sorted right_sorted

    pairs |> Seq.map (fun (l, r) -> Math.Abs(l - r)) |> Seq.sum


printfn $"%A{part1}"

let part2 =
    let leftList, rightList = getData ()

    let frequencies list =
        list
        |> Seq.fold
            (fun frequencyMap key ->
                match Map.tryFind key frequencyMap with
                | Some freq -> Map.add key (freq + 1) frequencyMap
                | None -> Map.add key 1 frequencyMap)
            Map.empty

    let leftFreq = frequencies leftList
    let rightFreq = frequencies rightList

    leftFreq.Keys
    |> Seq.filter rightFreq.ContainsKey
    |> Seq.sumBy (fun key -> key * leftFreq[key] * rightFreq[key])

printfn $"%A{part2}"
