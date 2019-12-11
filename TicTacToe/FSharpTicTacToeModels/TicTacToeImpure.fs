namespace QUT

    module FSharpImpureTicTacToeModel =
        open System.Numerics
    
        type Player = Nought | Cross 

        type GameState = 
            { Size: int; mutable Turn: Player; mutable Board: string[,] } 
            interface ITicTacToeGame<Player> with
                member this.Turn with get()    = this.Turn 
                member this.Size with get()    = this.Size 
                member this.getPiece(row, col) = 
                    Array2D.get this.Board row col

        type Move = 
            { Row: int; Column: int }
            interface ITicTacToeMove with
                member this.Row with get() = this.Row 
                member this.Col with get() = this.Column

        let Lines (size:int) : seq<seq<int*int>> = 
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

        let CheckLine (game:GameState) (line:seq<int*int>) : TicTacToeOutcome<Player> = 
            let lineList = [ // Creates a list containing "O"s or "X"s at the given coordinates at line
                for i = 0 to (Seq.length line) - 1 do
                    let row, col = Seq.item i line
                    yield Array2D.get game.Board row col
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
            
            let isWin outcome = 
                match outcome with
                | Win(_,_) -> true
                | _ -> false

            if not(Seq.tryFind isWin seqResults = None) then
                Seq.find isWin seqResults
            elif Seq.forall (fun x -> x = Draw) seqResults then
                Draw
            else
                Undecided
       
        let ApplyMove game move  = 
            if game.Turn = Nought then
                Array2D.set game.Board move.Row move.Column "O"
                game.Turn <- Cross
            else 
                Array2D.set game.Board move.Row move.Column "X"
                game.Turn <- Nought
            game
        
        let UndoMove game move = 
            Array2D.set game.Board move.Row move.Column ""
            if game.Turn = Nought then game.Turn <- Cross
            else game.Turn <- Nought
            game

        let CreateMove row col   = 
            { Row=row; Column=col }
        
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
            seq {
                for row = 0 to game.Size - 1 do
                    for col = 0 to game.Size - 1 do
                        if Array2D.get game.Board row col = "" then
                            yield CreateMove row col
            }

        let MiniMaxWithAlphaBetaPruningGenerator (heuristic:'Game -> 'Player -> int) (getTurn: 'Game -> 'Player) (gameOver:'Game->bool) (moveGenerator: 'Game->seq<'Move>) (applyMove: 'Game -> 'Move -> 'Game) (undoMove: 'Game -> 'Move -> 'Game) : int -> int -> 'Game -> 'Player -> Option<'Move> * int =
            // Basic MiniMax algorithm without using alpha-beta pruning
            let rec MiniMax alpha beta game perspective =
                NodeCounter.Increment()
                // BASE CASE: Checking a leaf node
                if gameOver game then 
                    (None, heuristic game perspective) // Returns a tuple of (Move, Heuristic)

                else // RECURSIVE CASE: Checking a parent node
                    let childMoveList = [
                            for i in moveGenerator game do
                                yield i
                        ]
                    //let childStateList = List.map (fun x -> (x, applyMove game x)) childMoveList // 2. Apply the moves to get the resulting GameStates
                    let mutable oldState = game

                    let rec findMax alpha beta childMoveList child = 
                        // Setting up the head and tail to pass recursively
                        let moveListHead = List.head childMoveList
                        let moveListTail = List.tail childMoveList

                        // Getting the (Move, Heuristic) of the Head
                        let miniMax = MiniMax alpha beta (applyMove oldState moveListHead) perspective
                        oldState <- undoMove game moveListHead
                        let currentValue = (moveListHead, snd miniMax)

                        // Setting up the initial alpha tuple (Previous Move, Alpha)
                        let mutable a = (fst child, alpha)

                        let bestVal = 
                            if (snd child) >= (snd currentValue) then
                                child
                            else 
                                currentValue

                        a <- 
                            if (snd a) >= (snd bestVal) then
                                a
                            else 
                                bestVal

                        if beta <= snd a || moveListTail.IsEmpty then
                            bestVal
                        else findMax (snd a) beta moveListTail bestVal
                    
                    let rec findMin alpha beta childMoveList child = 
                        // Setting up the head and tail to pass recursively
                        let moveListHead = List.head childMoveList
                        let moveListTail = List.tail childMoveList

                        // Getting the (Move, Heuristic) of the Head
                        let miniMax = MiniMax alpha beta (applyMove oldState moveListHead) perspective
                        oldState <- undoMove game moveListHead
                        let currentValue = (moveListHead, snd miniMax)

                        // Setting up the initial alpha tuple (Previous Move, Alpha)
                        let mutable b = (fst child, beta)

                        let bestVal = 
                            if (snd child) <= (snd currentValue) then
                                child
                            else 
                                currentValue

                        b <- 
                            if (snd b) <= (snd bestVal) then
                                b
                            else 
                                bestVal

                        if snd b <= alpha || moveListTail.IsEmpty then
                            bestVal
                        else findMin alpha (snd b) moveListTail bestVal

                    if getTurn game = perspective then
                        let (move, heuristic) = findMax alpha beta childMoveList (childMoveList.Head, alpha)
                        (Some move, heuristic)
                    else
                        let (move, heuristic) = findMin alpha beta childMoveList (childMoveList.Head, beta)
                        (Some move, heuristic)

            NodeCounter.Reset()
            MiniMax 

        let MiniMax game = (MiniMaxWithAlphaBetaPruningGenerator heuristic getTurn gameOver moveGenerator ApplyMove UndoMove) game

        let FindBestMove game    = 
            let (move, _) = MiniMax -1 1 game (getTurn game)
            move.Value

        let GameStart first size =
            let boardArr = Array2D.init size size (fun x y -> "")
            { Size=size; Turn=first; Board=boardArr}

        // plus other helper functions ...
        type WithAlphaBetaPruning() =
            override this.ToString()         = "Impure F# with Alpha Beta Pruning";
            interface ITicTacToeModel<GameState, Move, Player> with
                member this.Cross with get()             = Cross//raise (System.NotImplementedException("getCross"))
                member this.Nought with get()            = Nought//raise (System.NotImplementedException("getNought"))
                member this.GameStart(firstPlayer, size) = GameStart firstPlayer size
                member this.CreateMove(row, col)         = CreateMove row col
                member this.GameOutcome(game)            = GameOutcome game 
                member this.ApplyMove(game, move)        = ApplyMove game  move
                member this.FindBestMove(game)           = FindBestMove game