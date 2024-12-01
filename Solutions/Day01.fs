module Year2024Day01

open System
open System.IO
open System.Collections.Generic
open System.Linq

type Data() =
    member x.Read() =
        use stream = new StreamReader(@"../../../input.txt")

        let leftList = PriorityQueue<int, int>()
        let rightList = PriorityQueue<int, int>()

        let mutable valid = true

        while valid do
            let line = stream.ReadLine()

            if (line = null) then
                valid <- false
            else
                let split = line.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries)

                if split.Length = 2 then
                    let l = Convert.ToInt32(split[0])
                    let r = Convert.ToInt32(split[1])
                    leftList.Enqueue(l, l)
                    rightList.Enqueue(r, r)

        leftList, rightList

let getData () =
    let data = Data().Read()
    data.Deconstruct()

let part1 =
    let leftList, rightList = getData ()

    let processQueue (leftList: PriorityQueue<int, int>, rightList: PriorityQueue<int, int>) =
        if leftList.Count > 0 && rightList.Count > 0 then
            let left = leftList.Dequeue()
            let right = rightList.Dequeue()
            Some(Math.Abs(left - right), (leftList, rightList))
        else
            None

    Seq.unfold processQueue (leftList, rightList) |> Seq.sum

printfn $"%A{part1}"

let part2 =
    let leftQueue, rightQueue = getData ()

    let toList (queue: PriorityQueue<int, int>) =
        Enumerable.Range(0, queue.Count).Select(fun _ -> queue.Dequeue())

    let leftItems = toList leftQueue
    let rightItems = toList rightQueue

    let frequencyCounter source =
        (Map.empty<int, int>, source)
        ||> Seq.fold (fun map item -> map.Add(item, map.GetValueOrDefault(item) + 1))

    let leftFreq = frequencyCounter leftItems
    let rightFreq = frequencyCounter rightItems

    let commonKeys = ((fun l -> rightFreq.ContainsKey(l)), leftFreq.Keys) ||> Seq.filter

    let calculateSimilarity =
        fun key ->
            if leftFreq.ContainsKey key && rightFreq.ContainsKey key then
                Some(key * leftFreq[key] * rightFreq[key])
            else
                None
                
    Seq.choose
        calculateSimilarity
        commonKeys
    |> Seq.sum

printfn $"%A{part2}"
