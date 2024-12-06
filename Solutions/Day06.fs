module Year2024Day06

open System.IO
open System.Collections

type Data() =
    member x.Read() =

        let lines =
            File.ReadLines(@"../../../day06_input.txt")
            |> List.ofSeq
            |> List.map (_.ToCharArray())

        let grid = Array2D.init lines.Length lines[0].Length (fun i j -> lines[i][j])

        let initialDirection, initialPosition =
            [ for i in 0 .. grid.GetLength(0) - 1 do
                  for j in 0 .. grid.GetLength(1) - 1 do
                      match grid[i, j] with
                      | '>' -> Some((0, 1), (i, j))
                      | '<' -> Some((0, -1), (i, j))
                      | '^' -> Some((-1, 0), (i, j))
                      | 'v' -> Some((1, 0), (i, j))
                      | _ -> None ]
            |> List.filter (_.IsSome)
            |> List.head
            |> _.Value

        grid, initialDirection, initialPosition

let rec pathToExit acc (dir: int * int) (pos: int * int) (grid: char array2d) =
    let newPosition = (fst pos + fst dir), (snd pos + snd dir)
    let rows = grid.GetLength 0
    let cols = grid.GetLength 1

    match newPosition with
    | i, j when i < 0 || i >= rows || j < 0 || j >= cols -> pos :: acc
    | i, j when grid[i, j] <> '#' -> pathToExit (pos :: acc) dir newPosition grid
    | i, j when grid[i, j] = '#' ->
        let newDir =
            match dir with
            | (-1, 0) -> (0, 1)
            | (0, 1) -> (1, 0)
            | (1, 0) -> (0, -1)
            | (0, -1) -> (-1, 0)

        let newPosition2 = (fst pos + fst newDir), (snd pos + snd newDir)

        pathToExit (pos :: acc) newDir newPosition2 grid

let part1 =
    let grid, dir, pos = Data().Read()

    let path = pathToExit list.Empty dir pos grid

    path |> List.groupBy id |> List.length


printfn $"%A{part1}"

let part2 =
    let grid, dir, pos = Data().Read()

    let path = pathToExit list.Empty dir pos grid

    let potentialObstaclePositions = path |> List.groupBy id |> List.map fst
    
    ()

printfn $"%A{part2}"
