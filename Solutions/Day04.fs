module Year2024Day04

open System.IO

type Data() =
    member x.Read() =
        let filename = @"../../../day04_input.txt"

        let lines = File.ReadLines(filename) |> List.ofSeq |> List.map (_.ToCharArray())
        Array2D.init lines.Length lines[0].Length (fun i j -> lines[i][j])

let part1 =
    let lines = Data().Read()

    let mutable count = 0

    let isXmas (s: char array) =
        (s[0] = 'X' && s[1] = 'M' && s[2] = 'A' && s[3] = 'S')
        || (s[0] = 'S' && s[1] = 'A' && s[2] = 'M' && s[3] = 'X')

    for i in 0 .. (lines.GetLength 0) - 1 do
        for j in 0 .. (lines.GetLength 1) - 1 do

            if lines[i, j] <> 'X' && lines[i, j] <> 'S' then
                ()
            else

                let shouldCheckVertical = i < (lines.GetLength 0) - 3
                let shouldCheckHorizontal = j < (lines.GetLength 1) - 3
                let shouldCheckMainDiag = shouldCheckHorizontal && shouldCheckVertical
                let shouldCheckSecondDiag = shouldCheckVertical && 3 <= j

                let mutable slices = List.empty

                if shouldCheckHorizontal && isXmas lines[i, j .. j + 3] then
                    count <- count + 1

                if shouldCheckVertical && isXmas lines[i .. i + 3, j] then
                    count <- count + 1

                if shouldCheckMainDiag
                    && isXmas [| lines[i, j]; lines[i + 1, j + 1]; lines[i + 2, j + 2]; lines[i + 3, j + 3] |]
                then
                    count <- count + 1

                if shouldCheckSecondDiag
                    && isXmas [| lines[i, j]; lines[i + 1, j - 1]; lines[i + 2, j - 2]; lines[i + 3, j - 3] |]
                then
                    count <- count + 1

    count

printfn $"%A{part1}"

let part2 =
    let lines = Data().Read()

    let isMas (three: char array) =
        match three with
        | [| 'M'; 'A'; 'S' |] -> true
        | [| 'S'; 'A'; 'M' |] -> true
        | _ -> false

    let isCrossMas (window: char array2d) =
        let d1 = [| window[0, 0]; window[1, 1]; window[2, 2] |]
        let d2 = [| window[0, 2]; window[1, 1]; window[2, 0] |]

        isMas d1 && isMas d2

    let windows =
        [ for i in 0 .. (lines.GetLength(0) - 3) do
              for j in 0 .. (lines.GetLength(1) - 3) do
                  yield isCrossMas lines[i .. i + 2, j .. j + 2] ]

    windows |> Seq.filter id |> Seq.length

printfn $"%A{part2}"
