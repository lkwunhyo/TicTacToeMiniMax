using System;
using System.Collections.Generic;

namespace QUT.CSharpTicTacToe
{
    public class WithAlphaBetaPruning : ITicTacToeModel<Game, Move, Player>
    {
        
        public Player Cross => Player.Cross;
        public Player Nought => Player.Nought;
        public override string ToString()
        {
            return "Impure C# with Alpha Beta Pruning";
        }
        public Game ApplyMove(Game game, Move move)
        {
            if (game.Turn == Nought)
            {
                game.Board[move.Row, move.Col] = "O";
                game.Turn = Cross;
            } else
            {
                game.Board[move.Row, move.Col] = "X";
                game.Turn = Nought;
            }
            return game;
        }
        public Game UndoMove(Game game, Move move)
        {
            if (game.Turn == Nought)
            {
                game.Board[move.Row, move.Col] = "";
                game.Turn = Cross;
            }
            else
            {
                game.Board[move.Row, move.Col] = "";
                game.Turn = Nought;
            }
            return game;
        }
        public Move CreateMove(int row, int col)
        {
            Move newMove = new Move(row, col);
            return newMove;
        }

        public int heuristic(Game game, Player player)
        {
            if (GameOutcome(game).IsWin)
            {
                var win = GameOutcome(game) as TicTacToeOutcome<Player>.Win;
                if (win.winner == player) return 1;
                else return -1;
            } else return 0;
        }

        public Player getTurn(Game game)
        {
            return game.Turn;
        }
        
        public bool gameOver(Game game)
        {
            if (GameOutcome(game) == TicTacToeOutcome<Player>.Undecided)
            {
                return true;
            } else {
                return false;
                    }
        }
        
        public List<Move> moveGenerator(Game game)
        {
            List<Move> moveList = new List<Move>();
            for (int row = 0; row <= game.Size - 1; row++)
            {
                for (int col = 0; col <= game.Size - 1; col++)
                {
                    if (game.Board[row, col] == "")
                    {
                        moveList.Add(CreateMove(row, col));
                    }
                }
            }
            return moveList;
        }

        public Tuple<Move, int> MiniMaxWithAlphaBeta(int alpha, int beta, Game game, Player perspective) //int alpha, int beta, Game game, Player perspective)
        {
            NodeCounter.Increment();
            // Generating child nodes
            Move nullMove = new Move();
            if (gameOver(game))
            {
                return Tuple.Create(nullMove, heuristic(game, perspective));
            }
            else
            {
                List<Move> moveList = moveGenerator(game);
                Game oldState = game;

                if (getTurn(game) == perspective)
                {
                    Tuple<Move, int> bestVal = Tuple.Create(nullMove, Int32.MinValue);
                    foreach (Move move in moveList)
                    {
                        Tuple<Move, int> miniMax = MiniMaxWithAlphaBeta(alpha, beta, (ApplyMove(oldState, move)), perspective);
                        oldState = UndoMove(game, move);
                        Tuple<Move, int> currentValue = Tuple.Create(move, miniMax.Item2);

                        // Setting up the initial alpha tuple (Previous Move, Alpha)
                        Tuple<Move, int> a;
                        if (moveList.FindIndex(x => x == move) == 0)
                        {
                            a = Tuple.Create(nullMove, alpha);
                        }
                        else
                        {
                            a = Tuple.Create(moveList[moveList.FindIndex(x => x == move) - 1], alpha);
                        }

                        if (alpha >= currentValue.Item2)
                        {
                            bestVal = a;
                        }
                        else
                        {
                            bestVal = currentValue;
                        }

                        if (a.Item2 <= bestVal.Item2)
                        {
                            a = bestVal;
                        }

                        if (beta <= a.Item2)
                        {
                            break;
                        }
                    }
                    return bestVal;
                }
                else
                {
                    Tuple<Move, int> bestVal = Tuple.Create(nullMove, Int32.MinValue);
                    foreach (Move move in moveList)
                    {
                        Tuple<Move, int> miniMax = MiniMaxWithAlphaBeta(alpha, beta, (ApplyMove(oldState, move)), perspective);
                        oldState = UndoMove(game, move);
                        Tuple<Move, int> currentValue = Tuple.Create(move, miniMax.Item2);

                        // Setting up the initial alpha tuple (Previous Move, Alpha)
                        Tuple<Move, int> b;
                        if (moveList.FindIndex(x => x == move) == 0)
                        {
                            b = Tuple.Create(nullMove, beta);
                        }
                        else
                        {
                            b = Tuple.Create(moveList[moveList.FindIndex(x => x == move) - 1], beta);
                        }

                        if (beta <= currentValue.Item2)
                        {
                            bestVal = b;
                        }
                        else
                        {
                            bestVal = currentValue;
                        }

                        if (b.Item2 >= bestVal.Item2)
                        {
                            b = bestVal;
                        }

                        if (b.Item2 <= alpha)
                        {
                            break;
                        }

                    }
                    return bestVal;
                }
            }
        }

        public Move FindBestMove(Game game)
        {
            return MiniMaxWithAlphaBeta(-1, 1, game, game.Turn).Item1;
        }

        public TicTacToeOutcome<Player> GameOutcome(Game game)
        {
            // Winning Lines
            List<List<Tuple<int, int>>> boardList = new List<List<Tuple<int, int>>>();

            for (int i = 0; i <= game.Size - 1; i++)
            {
                // HORIZONTAL LINE
                List<Tuple<int, int>> horizontalSeq = new List<Tuple<int, int>>();
                for (int j = 0; j <= game.Size - 1; j++)
                {
                    horizontalSeq.Add(Tuple.Create(i, j));
                }
                boardList.Add(horizontalSeq);
            }
            // VERTICAL LINE
            for (int i = 0; i <= game.Size - 1; i++)
            {
                List<Tuple<int, int>> verticalSeq = new List<Tuple<int, int>>();
                for (int j = 0; j <= game.Size - 1; j++)
                {
                    verticalSeq.Add(Tuple.Create(j, i));
                }
                boardList.Add(verticalSeq);
            }

            // DIAGONAL LINE \
            List<Tuple<int, int>> diagonalLeftSeq = new List<Tuple<int, int>>();
            for (int i = 0; i <= game.Size - 1; i++)
            {
                diagonalLeftSeq.Add(Tuple.Create(i, i));
            }
            boardList.Add(diagonalLeftSeq);

            // DIAGONAL LINE /
            List<Tuple<int, int>> diagonalRightSeq = new List<Tuple<int, int>>();
            for (int i = 0; i <= game.Size - 1; i++)
            {
                diagonalRightSeq.Add(Tuple.Create(i, game.Size - 1 - i));
            }
            boardList.Add(diagonalRightSeq);

            List<TicTacToeOutcome<Player>> outcomeList = new List<TicTacToeOutcome<Player>>();
            foreach (List<Tuple<int, int>> lineList in boardList)
            {
                List<string> currentStateList = new List<string>();
                foreach (Tuple<int, int> i in lineList) {
                    currentStateList.Add(game.Board[i.Item1, i.Item2]);
                }

                if (currentStateList.TrueForAll(x => x == "O")) {
                    outcomeList.Add(TicTacToeOutcome<Player>.NewWin(Nought, lineList));
                } else if (currentStateList.TrueForAll(x => x == "X")) {
                    outcomeList.Add(TicTacToeOutcome<Player>.NewWin(Cross, lineList));
                } else if (currentStateList.TrueForAll(x => x == "")) {
                    outcomeList.Add(TicTacToeOutcome<Player>.Undecided);
                } else {
                    if (currentStateList.Contains("O") && currentStateList.Contains("X")) {
                        outcomeList.Add(TicTacToeOutcome<Player>.Draw);
                    } else {
                        outcomeList.Add(TicTacToeOutcome<Player>.Undecided);
                    }
                }
            }

            // If it contains a win, return win
            foreach (TicTacToeOutcome<Player> i in outcomeList)
            {
                if (i.IsWin) return i;
            }
            if (outcomeList.TrueForAll(x => x.IsDraw))
            {
                return TicTacToeOutcome<Player>.Draw;
            }
            else
            {
                return TicTacToeOutcome<Player>.Undecided;
            }
        }

        public Game GameStart(Player first, int size)
        {
            Game newGame = new Game(first, size);
            return newGame;
        }
    }
}