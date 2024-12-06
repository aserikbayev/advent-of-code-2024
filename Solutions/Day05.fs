module Year2024Day05

open System
open System.Collections.Generic
open System.IO

type Data() =
    member x.Read() =
        let filename = @"../../../day05_input.txt"

        let lines = File.ReadLines(filename) |> List.ofSeq

        let idx = lines |> List.findIndex (fun x -> x = "")
        let rules, updates = lines |> List.splitAt idx

        let rulesMap =
            rules
            |> List.map (fun rule ->
                let splits = rule.Split("|")
                int splits[0], int splits[1])
            |> List.groupBy fst
            |> List.map (fun (key, pairs) -> key, (pairs |> List.map snd))
            |> Map.ofList

        let updatesInt =
            (updates |> List.tail) // remove "" after split
            |> List.map (fun line -> line.Split(",") |> Array.map int |> Array.toList)

        rulesMap, updatesInt

let rec isOrderedAccordingTo rules (lst: int list) =
    match lst with
    | [] -> true
    | x :: xs ->
        match Map.tryFind x rules with
        | Some successors ->
            let areAllSuccessors = xs |> List.forall (fun item -> List.contains item successors)
            areAllSuccessors && isOrderedAccordingTo rules xs
        | None -> false

let getMiddleElement oddLengthList =
    let len = List.length oddLengthList
    oddLengthList[len / 2]

let part1 =
    let rules, updates = Data().Read()

    updates
    |> List.filter (isOrderedAccordingTo rules)
    |> getMiddleElement
    |> List.sum

printfn $"%A{part1}"

let part2 =
    let rules, updates = Data().Read()

    let sortBy rules nodes =
        let rec sortBy acc nodes =
            if List.length nodes = 0 then
                acc
            else
                let current = List.head nodes
                let remaining = List.tail nodes

                match Map.tryFind current rules with
                | Some suc ->
                    // if remaining follow current, keep going
                    if List.forall (fun r -> List.contains r suc) remaining then
                        sortBy (current :: acc) remaining
                    else
                        sortBy acc (remaining @ [ current ])
                | None -> sortBy acc (remaining @ [ current ])

        sortBy [] nodes

    let incorrectlyOrderedUpdates =
        updates |> List.filter (fun update -> not (isOrderedAccordingTo rules update))

    incorrectlyOrderedUpdates
    |> List.map (sortBy rules)
    |> List.map getMiddleElement
    |> List.sum

printfn $"%A{part2}"
