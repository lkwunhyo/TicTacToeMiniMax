namespace QUT

    module GameTheory =
        open System.Linq.Expressions
        open System.Diagnostics

        let MiniMaxGenerator (heuristic:'Game -> 'Player -> int) (getTurn: 'Game -> 'Player) (gameOver:'Game->bool) (moveGenerator: 'Game->seq<'Move>) (applyMove: 'Game -> 'Move -> 'Game) : 'Game -> 'Player -> Option<'Move> * int =
            // Basic MiniMax algorithm without using alpha beta pruning
            let rec MiniMax game perspective =
                NodeCounter.Increment()

                // BASE CASE: Checking a leaf node
                if gameOver game then 
                    (None, heuristic game perspective) // Returns a tuple of (Move, Heuristic)

                else // RECURSIVE CASE: Checking a parent node
                    // 1. Generate the child moves
                    let childMoveList = [
                            for i in moveGenerator game do
                                yield i
                        ]
                    let miniMaxList = 
                        childMoveList
                        |> List.map (fun x -> (x, applyMove game x)) // 2. Apply the moves to get the resulting GameStates
                        |> List.map (fun (move, game) -> (move, MiniMax game perspective))   // 3. Recursively call MiniMax on each of the GameStates
                    
                    // 4. Pick the best child
                    let findBest limit bestMiniMax = 
                        let maxIndex list =  // Searches for the index of a max value
                            list
                            |> List.mapi (fun i x -> i, x)
                            |> List.maxBy snd 
                            |> fst
                        let minIndex list =  // Searches for the index of a max value
                            list
                            |> List.mapi (fun i x -> i, x)
                            |> List.minBy snd 
                            |> fst
                        // Creates a sequence of heuristics of the new GameStates
                        let heuristicList = List.map (fun (_, (_, heuristic)) -> heuristic) bestMiniMax

                        // Searches for a MAX or MIN GameState by the index of the MAX/MIN heuristic
                        if limit = "MAX" then 
                            let (move, (_, heuristic)) = Seq.item (maxIndex heuristicList) bestMiniMax
                            (Some move, heuristic)
                        else
                            let (move, (_, heuristic)) = Seq.item (minIndex heuristicList) bestMiniMax
                            (Some move, heuristic)
                        
                    // Returns a MiniMax tuple depending on the current player
                    if getTurn game = perspective then
                        findBest "MAX" miniMaxList
                    else
                        findBest "MIN" miniMaxList
                    
            NodeCounter.Reset()
            MiniMax

        let MiniMaxWithAlphaBetaPruningGenerator (heuristic:'Game -> 'Player -> int) (getTurn: 'Game -> 'Player) (gameOver:'Game->bool) (moveGenerator: 'Game->seq<'Move>) (applyMove: 'Game -> 'Move -> 'Game) : int -> int -> 'Game -> 'Player -> Option<'Move> * int =
            // Optimized MiniMax algorithm that uses alpha beta pruning to eliminate parts of the search tree that don't need to be explored            
            let rec MiniMax alpha beta oldState perspective =
                NodeCounter.Increment()

                let childMoveList = [
                            for i in moveGenerator oldState do
                                yield i
                        ]
                let childStateList = List.map (fun x -> (x, applyMove oldState x)) childMoveList// 2. Apply the moves to get the resulting GameStates

                // BASE CASE: Checking a leaf node
                if gameOver oldState then 
                    (None, heuristic oldState perspective) // Returns a tuple of (Move, Heuristic)

                else // RECURSIVE CASE: Checking a parent node
                    // Maximising Player Recursive Function                    
                    let rec findMax alpha beta moveGameList child = 
                        // Setting up the head and tail to pass recursively
                        let moveGameHead = List.head moveGameList
                        let moveGameTail = List.tail moveGameList
                        let (currentMove, currentGame) = moveGameHead
                        let (childMove, _) = child

                        // Getting the (Move, Heuristic) of the Head
                        let miniMax = MiniMax alpha beta currentGame perspective
                        let currentValue = (currentMove, snd miniMax)

                        // Setting up the initial alpha tuple (Previous Move, Alpha)
                        let a = (childMove, alpha)

                        // bestVal = max(previousNode, currentNode)
                        let bestVal = 
                            if (snd child) >= (snd currentValue) then
                                child
                            else 
                                currentValue
                        
                        // alpha = max(alpha, bestNode)
                        let new_a = 
                            if (snd a) >= (snd bestVal) then
                                a
                            else 
                                bestVal

                        if beta <= snd new_a || moveGameTail.IsEmpty then
                            bestVal
                        else findMax (snd new_a) beta moveGameTail bestVal                    

                    // Minimising Player Recursive Function
                    let rec findMin alpha beta moveGameList child = 
                        let moveGameHead = List.head moveGameList
                        let moveGameTail = List.tail moveGameList
                        let (currentMove, currentGame) = moveGameHead
                        let (childMove, _) = child

                        let miniMax = MiniMax alpha beta currentGame perspective
                        let currentValue = (currentMove, snd miniMax)

                        let b = (childMove, beta)

                        let bestVal = 
                            if (snd child) <= (snd currentValue) then
                                child
                            else 
                                currentValue

                        let new_b = 
                            if (snd b) <= (snd bestVal) then
                                b
                            else 
                                bestVal

                        if snd new_b <= alpha || moveGameTail.IsEmpty then
                            bestVal
                        else findMin alpha (snd new_b) moveGameTail bestVal 

                    if getTurn oldState = perspective then
                        let (move, heuristic) = findMax alpha beta childStateList (fst childStateList.Head, alpha)
                        (Some move, heuristic)
                    else
                        let (move, heuristic) = findMin alpha beta childStateList (fst childStateList.Head, beta)
                        (Some move, heuristic)

            NodeCounter.Reset()
            MiniMax
