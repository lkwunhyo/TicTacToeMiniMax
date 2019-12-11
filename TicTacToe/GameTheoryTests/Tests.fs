namespace QUT

    open Microsoft.VisualStudio.TestTools.UnitTesting

    [<TestClass>]
    type GameTheoryTests () =

        let rec Heuristic state perspective =
            if (perspective = "fred") then
                match state with
                | 19 -> 5
                | 20 -> 6
                | 21 -> 7
                | 22 -> 4
                | 23 -> 5
                | 24 -> 3
                | 25 -> 6
                | 26 -> 6
                | 27 -> 9
                | 28 -> 7
                | 29 -> 5
                | 30 -> 9
                | 31 -> 8
                | 32 -> 6
                | _ -> raise (System.ArgumentException("invalid state"))
            else
                raise (System.ArgumentException("this heuristic is only defined from fred's perspective"))

        let rec Heuristic2 state perspective =
            if (perspective = "fred") then
                match state with
                | 19 -> 5
                | 20 -> 6
                | 21 -> 7
                | 22 -> 4
                | 24 -> 3
                | 25 -> 6
                | 26 -> 6
                | 28 -> 7
                | 29 -> 5
                | _ -> raise (System.ArgumentException(state.ToString() + " should be pruned when using alpha beta pruning"))
            else
                raise (System.ArgumentException("this heuristic is only defined from fred's perspective"))

        let NextTurn = function
        | 0 -> "fred"
        | state when 1 <= state && state <= 3 -> "jill"
        | state when 4 <= state && state <= 9 -> "fred"
        | state when 10 <= state && state <= 18 -> "jill"
        | state when 19 <= state && state <= 32 -> "fred"
        | _ -> raise (System.ArgumentException("invalid state"))

        let GameOver state = state > 18

        let MoveGenerator = function
            | 0 -> seq [ 'a'; 'b'; 'c' ]
            | 1 -> seq [ 'd'; 'e' ]
            | 2 -> seq [ 'f'; 'g' ]
            | 3 -> seq [ 'h'; 'i' ]
            | 4 -> seq [ 'j'; 'k' ]
            | 5 -> seq [ 'l' ]
            | 6 -> seq [ 'm'; 'n' ]
            | 7 -> seq [ 'o' ]
            | 8 -> seq [ 'p' ]
            | 9 -> seq [ 'q'; 'r' ]
            | 10 -> seq [ 's'; 't' ]
            | 11 -> seq [ 'u'; 'v'; 'w' ]
            | 12 -> seq [ 'x' ]
            | 13 -> seq [ 'y' ]
            | 14 -> seq [ 'z'; 'A' ]
            | 15 -> seq [ 'B'  ]
            | 16 -> seq [ 'C' ]
            | 17 -> seq [ 'D'; 'E' ]
            | 18 -> seq [ 'F' ]
            | _  -> seq [ ]

        let ApplyMove state move =
            match (state, move) with
            | 0, 'a' -> 1
            | 0, 'b' -> 2
            | 0, 'c' -> 3
            | 1, 'd' -> 4
            | 1, 'e' -> 5
            | 2, 'f' -> 6
            | 2, 'g' -> 7
            | 3, 'h' -> 8
            | 3, 'i' -> 9
            | 4, 'j' -> 10
            | 4, 'k' -> 11
            | 5, 'l' -> 12
            | 6, 'm' -> 13
            | 6, 'n' -> 14
            | 7, 'o' -> 15
            | 8, 'p' -> 16
            | 9, 'q' -> 17
            | 9, 'r' -> 18
            | 10, 's' -> 19
            | 10, 't' -> 20
            | 11, 'u' -> 21
            | 11, 'v' -> 22
            | 11, 'w' -> 23
            | 12, 'x' -> 24
            | 13, 'y' -> 25
            | 14, 'z' -> 26
            | 14, 'A' -> 27
            | 15, 'B' -> 28
            | 16, 'C' -> 29
            | 17, 'D' -> 30
            | 17, 'E' -> 31
            | 18, 'F' -> 32
            | _ -> raise (System.ArgumentException("unexpected state, move combination"))

        let noMove : Option<char> = None

        let BasicMiniMax = GameTheory.MiniMaxGenerator Heuristic NextTurn GameOver MoveGenerator ApplyMove

        let AlphaBetaPruning = GameTheory.MiniMaxWithAlphaBetaPruningGenerator Heuristic NextTurn GameOver MoveGenerator ApplyMove System.Int32.MinValue System.Int32.MaxValue

        let AlphaBetaPruningWithChecks = GameTheory.MiniMaxWithAlphaBetaPruningGenerator Heuristic2 NextTurn GameOver MoveGenerator ApplyMove System.Int32.MinValue System.Int32.MaxValue


        // Check that MiniMax produces the correct answer

        [<TestMethod>] member this.GameTheory019 () = Assert.AreEqual((noMove, 5), BasicMiniMax 19 "fred")
        [<TestMethod>] member this.GameTheory020 () = Assert.AreEqual((noMove, 6), BasicMiniMax 20 "fred")
        [<TestMethod>] member this.GameTheory021 () = Assert.AreEqual((noMove, 7), BasicMiniMax 21 "fred")
        [<TestMethod>] member this.GameTheory022 () = Assert.AreEqual((noMove, 4), BasicMiniMax 22 "fred")
        [<TestMethod>] member this.GameTheory023 () = Assert.AreEqual((noMove, 5), BasicMiniMax 23 "fred")
        [<TestMethod>] member this.GameTheory024 () = Assert.AreEqual((noMove, 3), BasicMiniMax 24 "fred")
        [<TestMethod>] member this.GameTheory025 () = Assert.AreEqual((noMove, 6), BasicMiniMax 25 "fred")
        [<TestMethod>] member this.GameTheory026 () = Assert.AreEqual((noMove, 6), BasicMiniMax 26 "fred")
        [<TestMethod>] member this.GameTheory027 () = Assert.AreEqual((noMove, 9), BasicMiniMax 27 "fred")
        [<TestMethod>] member this.GameTheory028 () = Assert.AreEqual((noMove, 7), BasicMiniMax 28 "fred")
        [<TestMethod>] member this.GameTheory029 () = Assert.AreEqual((noMove, 5), BasicMiniMax 29 "fred")
        [<TestMethod>] member this.GameTheory030 () = Assert.AreEqual((noMove, 9), BasicMiniMax 30 "fred")
        [<TestMethod>] member this.GameTheory031 () = Assert.AreEqual((noMove, 8), BasicMiniMax 31 "fred")
        [<TestMethod>] member this.GameTheory032 () = Assert.AreEqual((noMove, 6), BasicMiniMax 32 "fred")

        [<TestMethod>] member this.GameTheory010 () = Assert.AreEqual((Some 's', 5), BasicMiniMax 10 "fred")
        [<TestMethod>] member this.GameTheory011 () = Assert.AreEqual((Some 'v', 4), BasicMiniMax 11 "fred")
        [<TestMethod>] member this.GameTheory012 () = Assert.AreEqual((Some 'x', 3), BasicMiniMax 12 "fred")
        [<TestMethod>] member this.GameTheory013 () = Assert.AreEqual((Some 'y', 6), BasicMiniMax 13 "fred")
        [<TestMethod>] member this.GameTheory014 () = Assert.AreEqual((Some 'z', 6), BasicMiniMax 14 "fred")
        [<TestMethod>] member this.GameTheory015 () = Assert.AreEqual((Some 'B', 7), BasicMiniMax 15 "fred")
        [<TestMethod>] member this.GameTheory016 () = Assert.AreEqual((Some 'C', 5), BasicMiniMax 16 "fred")
        [<TestMethod>] member this.GameTheory017 () = Assert.AreEqual((Some 'E', 8), BasicMiniMax 17 "fred")
        [<TestMethod>] member this.GameTheory018 () = Assert.AreEqual((Some 'F', 6), BasicMiniMax 18 "fred")

        [<TestMethod>] member this.GameTheory004 () = Assert.AreEqual((Some 'j', 5), BasicMiniMax 4 "fred")
        [<TestMethod>] member this.GameTheory005 () = Assert.AreEqual((Some 'l', 3), BasicMiniMax 5 "fred")
        [<TestMethod>] member this.GameTheory006 () = Assert.AreEqual((Some 'm', 6), BasicMiniMax 6 "fred")
        [<TestMethod>] member this.GameTheory007 () = Assert.AreEqual((Some 'o', 7), BasicMiniMax 7 "fred")
        [<TestMethod>] member this.GameTheory008 () = Assert.AreEqual((Some 'p', 5), BasicMiniMax 8 "fred")
        [<TestMethod>] member this.GameTheory009 () = Assert.AreEqual((Some 'q', 8), BasicMiniMax 9 "fred")

        [<TestMethod>] member this.GameTheory001 () = Assert.AreEqual((Some 'e', 3), BasicMiniMax 1 "fred")
        [<TestMethod>] member this.GameTheory002 () = Assert.AreEqual((Some 'f', 6), BasicMiniMax 2 "fred")
        [<TestMethod>] member this.GameTheory003 () = Assert.AreEqual((Some 'h', 5), BasicMiniMax 3 "fred")

        [<TestMethod>] member this.GameTheory000 () = Assert.AreEqual((Some 'b', 6),  BasicMiniMax 0 "fred")

        // Check that Alpha Beta pruning produces the same answer as MiniMax

        [<TestMethod>] member this.GameTheory119 () = Assert.AreEqual((noMove, 5), AlphaBetaPruning 19 "fred")
        [<TestMethod>] member this.GameTheory120 () = Assert.AreEqual((noMove, 6), AlphaBetaPruning 20 "fred")
        [<TestMethod>] member this.GameTheory121 () = Assert.AreEqual((noMove, 7), AlphaBetaPruning 21 "fred")
        [<TestMethod>] member this.GameTheory122 () = Assert.AreEqual((noMove, 4), AlphaBetaPruning 22 "fred")
        [<TestMethod>] member this.GameTheory123 () = Assert.AreEqual((noMove, 5), AlphaBetaPruning 23 "fred")
        [<TestMethod>] member this.GameTheory124 () = Assert.AreEqual((noMove, 3), AlphaBetaPruning 24 "fred")
        [<TestMethod>] member this.GameTheory125 () = Assert.AreEqual((noMove, 6), AlphaBetaPruning 25 "fred")
        [<TestMethod>] member this.GameTheory126 () = Assert.AreEqual((noMove, 6), AlphaBetaPruning 26 "fred")
        [<TestMethod>] member this.GameTheory127 () = Assert.AreEqual((noMove, 9), AlphaBetaPruning 27 "fred")
        [<TestMethod>] member this.GameTheory128 () = Assert.AreEqual((noMove, 7), AlphaBetaPruning 28 "fred")
        [<TestMethod>] member this.GameTheory129 () = Assert.AreEqual((noMove, 5), AlphaBetaPruning 29 "fred")
        [<TestMethod>] member this.GameTheory130 () = Assert.AreEqual((noMove, 9), AlphaBetaPruning 30 "fred")
        [<TestMethod>] member this.GameTheory131 () = Assert.AreEqual((noMove, 8), AlphaBetaPruning 31 "fred")
        [<TestMethod>] member this.GameTheory132 () = Assert.AreEqual((noMove, 6), AlphaBetaPruning 32 "fred")

        [<TestMethod>] member this.GameTheory110 () = Assert.AreEqual((Some 's', 5), AlphaBetaPruning 10 "fred")
        [<TestMethod>] member this.GameTheory111 () = Assert.AreEqual((Some 'v', 4), AlphaBetaPruning 11 "fred")
        [<TestMethod>] member this.GameTheory112 () = Assert.AreEqual((Some 'x', 3), AlphaBetaPruning 12 "fred")
        [<TestMethod>] member this.GameTheory113 () = Assert.AreEqual((Some 'y', 6), AlphaBetaPruning 13 "fred")
        [<TestMethod>] member this.GameTheory114 () = Assert.AreEqual((Some 'z', 6), AlphaBetaPruning 14 "fred")
        [<TestMethod>] member this.GameTheory115 () = Assert.AreEqual((Some 'B', 7), AlphaBetaPruning 15 "fred")
        [<TestMethod>] member this.GameTheory116 () = Assert.AreEqual((Some 'C', 5), AlphaBetaPruning 16 "fred")
        [<TestMethod>] member this.GameTheory117 () = Assert.AreEqual((Some 'E', 8), AlphaBetaPruning 17 "fred")
        [<TestMethod>] member this.GameTheory118 () = Assert.AreEqual((Some 'F', 6), AlphaBetaPruning 18 "fred")

        [<TestMethod>] member this.GameTheory104 () = Assert.AreEqual((Some 'j', 5), AlphaBetaPruning 4 "fred")
        [<TestMethod>] member this.GameTheory105 () = Assert.AreEqual((Some 'l', 3), AlphaBetaPruning 5 "fred")
        [<TestMethod>] member this.GameTheory106 () = Assert.AreEqual((Some 'm', 6), AlphaBetaPruning 6 "fred")
        [<TestMethod>] member this.GameTheory107 () = Assert.AreEqual((Some 'o', 7), AlphaBetaPruning 7 "fred")
        [<TestMethod>] member this.GameTheory108 () = Assert.AreEqual((Some 'p', 5), AlphaBetaPruning 8 "fred")
        [<TestMethod>] member this.GameTheory109 () = Assert.AreEqual((Some 'q', 8), AlphaBetaPruning 9 "fred")

        [<TestMethod>] member this.GameTheory101 () = Assert.AreEqual((Some 'e', 3), AlphaBetaPruning 1 "fred")
        [<TestMethod>] member this.GameTheory102 () = Assert.AreEqual((Some 'f', 6), AlphaBetaPruning 2 "fred")
        [<TestMethod>] member this.GameTheory103 () = Assert.AreEqual((Some 'h', 5), AlphaBetaPruning 3 "fred")

        [<TestMethod>] member this.GameTheory100 () = Assert.AreEqual((Some 'b', 6),  AlphaBetaPruning 0 "fred")

        // Check that Alpha Beta pruning doesn't visit any unnecessary nodes ...

        [<TestMethod>] member this.GameTheory219 () = Assert.AreEqual((noMove, 5), AlphaBetaPruningWithChecks 19 "fred")
        [<TestMethod>] member this.GameTheory220 () = Assert.AreEqual((noMove, 6), AlphaBetaPruningWithChecks 20 "fred")
        [<TestMethod>] member this.GameTheory221 () = Assert.AreEqual((noMove, 7), AlphaBetaPruningWithChecks 21 "fred")
        [<TestMethod>] member this.GameTheory222 () = Assert.AreEqual((noMove, 4), AlphaBetaPruningWithChecks 22 "fred")
        [<TestMethod>] member this.GameTheory224 () = Assert.AreEqual((noMove, 3), AlphaBetaPruningWithChecks 24 "fred")
        [<TestMethod>] member this.GameTheory225 () = Assert.AreEqual((noMove, 6), AlphaBetaPruningWithChecks 25 "fred")
        [<TestMethod>] member this.GameTheory226 () = Assert.AreEqual((noMove, 6), AlphaBetaPruningWithChecks 26 "fred")
        [<TestMethod>] member this.GameTheory228 () = Assert.AreEqual((noMove, 7), AlphaBetaPruningWithChecks 28 "fred")
        [<TestMethod>] member this.GameTheory229 () = Assert.AreEqual((noMove, 5), AlphaBetaPruningWithChecks 29 "fred")

        [<TestMethod>] member this.GameTheory210 () = Assert.AreEqual((Some 's', 5), AlphaBetaPruningWithChecks 10 "fred")
        [<TestMethod>] member this.GameTheory212 () = Assert.AreEqual((Some 'x', 3), AlphaBetaPruningWithChecks 12 "fred")
        [<TestMethod>] member this.GameTheory213 () = Assert.AreEqual((Some 'y', 6), AlphaBetaPruningWithChecks 13 "fred")
        [<TestMethod>] member this.GameTheory215 () = Assert.AreEqual((Some 'B', 7), AlphaBetaPruningWithChecks 15 "fred")
        [<TestMethod>] member this.GameTheory216 () = Assert.AreEqual((Some 'C', 5), AlphaBetaPruningWithChecks 16 "fred")

        [<TestMethod>] member this.GameTheory204 () = Assert.AreEqual((Some 'j', 5), AlphaBetaPruningWithChecks 4 "fred")
        [<TestMethod>] member this.GameTheory205 () = Assert.AreEqual((Some 'l', 3), AlphaBetaPruningWithChecks 5 "fred")
        [<TestMethod>] member this.GameTheory206 () = Assert.AreEqual((Some 'm', 6), AlphaBetaPruningWithChecks 6 "fred")
        [<TestMethod>] member this.GameTheory207 () = Assert.AreEqual((Some 'o', 7), AlphaBetaPruningWithChecks 7 "fred")
        [<TestMethod>] member this.GameTheory208 () = Assert.AreEqual((Some 'p', 5), AlphaBetaPruningWithChecks 8 "fred")

        [<TestMethod>] member this.GameTheory201 () = Assert.AreEqual((Some 'e', 3), AlphaBetaPruningWithChecks 1 "fred")
        [<TestMethod>] member this.GameTheory202 () = Assert.AreEqual((Some 'f', 6), AlphaBetaPruningWithChecks 2 "fred")

        [<TestMethod>] member this.GameTheory200 () = Assert.AreEqual((Some 'b', 6),  AlphaBetaPruningWithChecks 0 "fred")
