namespace QUT

    module FSharpPureTicTacToeModel =
        open System.Linq
        open System.Linq
        open System.Runtime.InteropServices.ComTypes
    
        // type to represent the two players: Noughts and Crosses
        type Player = Nought | Cross

        // type to represent a single move specified using (row, column) coordinates of the selected square
        type Move = 
            { Row: int; Column: int }
            interface ITicTacToeMove with
                member this.Row with get() = this.Row
                member this.Col with get() = this.Column

        // type to represent the current state of the game, including the size of the game (NxN), who's turn it is and the pieces on the board
        type GameState = 
            { Size: int; Turn: Player; Board: List<List<string>> }
            interface ITicTacToeGame<Player> with
                member this.Turn with get()    = this.Turn
                member this.Size with get()    = this.Size
                member this.getPiece(row, col) = 
                    this.Board.[row].[col]

        let CreateMove row col = 
            { Row=row; Column=col }
            
        let ApplyMove (oldState:GameState) (move: Move) = 
            let newList = [
                for i = 0 to oldState.Board.Length - 1 do
                    yield [
                        for j = 0 to oldState.Board.[i].Length - 1 do
                            if move.Row = i && move.Column = j then
                                if oldState.Turn = Nought then
                                    yield "O"
                                else
                                    yield "X"
                            else 
                                yield oldState.Board.[i].[j]
                    ]
            ]
            
            // Returning the new GameState
            match oldState.Turn with
            | Nought -> { Size=oldState.Size; Turn=Cross; Board= newList}
            | Cross -> { Size=oldState.Size; Turn=Nought; Board= newList}

        // Returns a sequence containing all of the lines on the board: Horizontal, Vertical and Diagonal
        // The number of lines returned should always be (size*2+2)
        // the number of squares in each line (represented by (row,column) coordinates) should always be equal to size
        // For example, if the input size = 2, then the output would be: 
        //     seq [seq[(0,0);(0,1)];seq[(1,0);(1,1)];seq[(0,0);(1,0)];seq[(0,1);(1,1)];seq[(0,0);(1,1)];seq[(0,1);(1,0)]]
        // The order of the lines and the order of the squares within each line does not matter
        let Lines (size:int) : seq<seq<int*int>> = //raise (System.NotImplementedException("Lines"))
            let boardSeq = seq {
                for i = 0 to size - 1 do 
                    yield seq { // Horizontal line
                        for j = 0 to size - 1 do
                            yield (i, j)
                    }
                    yield seq { // Vertical line
                        for j = 0 to size - 1 do
                            yield (j, i)
                    }  
                yield seq { // Diagonal line \
                    for i=0 to size - 1 do
                        yield (i, i)
                }
                yield seq { // Diagonal line /
                    for i = 0 to size - 1 do
                        yield (i, size - 1 - i)                                  
                }
            }
            boardSeq

        // Checks a single line (specified as a sequence of (row,column) coordinates) to determine if one of the players
        // has won by filling all of those squares, or a Draw if the line contains at least one Nought and one Cross
        let CheckLine (game:GameState) (line:seq<int*int>) : TicTacToeOutcome<Player> = //raise (System.NotImplementedException("CheckLine"))
            let lineList = [ // Creates a list containing "O"s or "X"s at the given coordinates at line
                for i = 0 to (Seq.length line) - 1 do
                    let row, col = Seq.item i line
                    yield game.Board.[row].[col]
            ]

            let equalList list =    // Function to check equality of list (if all are "X" or "O")
                match list with
                | [] | [_] -> true
                | head::tail when tail |> List.forall((=) head) -> true
                | _ -> false

            if equalList lineList then  // Checking if there is a win
                if lineList.Head = "O" then
                    Win (Nought, line)
                elif lineList.Head = "X" then
                    Win (Cross, line)
                else // Incase all the cells are empty 
                    Undecided
            else    // Checking if it is at least a Draw
                if List.contains "O" lineList && List.contains "X" lineList then
                    Draw
                else // If it is not a draw, it is Undecided
                   Undecided
               
        let GameOutcome game =  
            
            let winSeq = Lines game.Size

            let seqResults = seq { // Returns a list of line checks
                    for i = 0 to Seq.length winSeq - 1 do
                        yield CheckLine game (Seq.item i winSeq)
                }
            
            let isWin outcome = //(outcome: TicTacToeOutcome<'Player>) = 
                match outcome with
                | Win(_,_) -> true
                | _ -> false

            if not(Seq.tryFind isWin seqResults = None) then
                Seq.find isWin seqResults
            elif Seq.forall (fun x -> x = Draw) seqResults then
                Draw
            else
                Undecided

        let GameStart (firstPlayer:Player) size = //raise (System.NotImplementedException("GameStart"))
            let boardList = [
                for i = 0 to size - 1 do
                    yield [
                        for j = 0 to size - 1 do
                            yield ""
                    ]
            ]
            { Size=size; Turn=firstPlayer; Board=boardList}
        
        // Generates a heuristic for a Win (-1 or 1) for a player and 0 for a Draw
        let heuristic game player = 
            match GameOutcome game with
            | Win(winner, _) -> if winner = player then 1 else -1
            | _ -> 0
        
        // Gets the current game turn
        let getTurn game = game.Turn

        // Checks if the game is over by checking GameOutcome = Undecided
        let gameOver game = if not(GameOutcome game = Undecided) then true else false

        // Generates a sequence of coordinates where the string is ""
        let moveGenerator game = 
            let moveSeq = seq {
                for row = 0 to game.Size - 1 do
                    for col = 0 to game.Size - 1 do
                        if List.item col (List.item row game.Board) = "" then
                            yield CreateMove row col
            }
            moveSeq
        
        let MiniMax game = 
            (GameTheory.MiniMaxGenerator (heuristic) (getTurn) (gameOver) (moveGenerator) (ApplyMove)) game  //raise (System.NotImplementedException("MiniMax"))
            
        let MiniMaxWithPruning game = //raise (System.NotImplementedException("MiniMaxWithPruning"))
            (GameTheory.MiniMaxWithAlphaBetaPruningGenerator (heuristic) (getTurn) (gameOver) (moveGenerator) (ApplyMove)) game  //raise (System.NotImplementedException("MiniMax"))
        // plus other helper functions ...

        [<AbstractClass>]
        type Model() =
            abstract member FindBestMove : GameState -> Move
            interface ITicTacToeModel<GameState, Move, Player> with
                member this.Cross with get()             = Cross 
                member this.Nought with get()            = Nought 
                member this.GameStart(firstPlayer, size) = GameStart firstPlayer size
                member this.CreateMove(row, col)         = CreateMove row col
                member this.GameOutcome(game)            = GameOutcome game
                member this.ApplyMove(game, move)        = ApplyMove game move 
                member this.FindBestMove(game)           = this.FindBestMove game

        type BasicMiniMax() =
            inherit Model()
            override this.ToString()         = "Pure F# with basic MiniMax";
            override this.FindBestMove(game) = 
                let (move, _) = MiniMax game game.Turn
                move.Value

        type WithAlphaBetaPruning() =
            inherit Model()
            override this.ToString()         = "Pure F# with Alpha Beta Pruning";
            override this.FindBestMove(game) = 
                let (move, _) = MiniMaxWithPruning -1 1 game game.Turn
                move.Value