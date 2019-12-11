using ReactiveUI;
using System.Reactive.Linq;
using System;
using System.Threading;
using ReactiveUI.Fody.Helpers;
using System.Threading.Tasks;

namespace QUT
{
    // This class contains the parts of the ViewModel that depend on the Game, Move and Player types from the Model
    // Separated in order to allow dependency injection without the View knowing about any of the different models
    public class TicTacToeViewModelChild<Game, Move, Player>: ReactiveObject, ITicTacToeViewModelChild where Game: ITicTacToeGame<Player> where Move : ITicTacToeMove
    {
        // A reference to the model
        private ITicTacToeModel<Game, Move, Player> TicTacToeModel;

        // the View Model of which this child is a part
        private TicTacToeViewModel parent;

        // The current state of the game from the model
        [Reactive] private Game gameState { get; set; }

        // Human is always Cross, Computer is always Nought
        private Player Human => TicTacToeModel.Cross;
        private Player Computer => TicTacToeModel.Nought;

        // defer to the model to check if the current game is over
        public bool IsGameOver => !TicTacToeModel.GameOutcome(gameState).IsUndecided;
        // defer to the model to check if it is currently the human's turn to make a move
        public bool IsHumanTurn => gameState != null && gameState.Turn.Equals(Human) && !IsGameOver;
        // defer to the model to check if it is currently the computer's turn to make a move
        public bool IsComputerTurn => gameState != null && gameState.Turn.Equals(Computer) && !IsGameOver;

        // Display name of this child, e.g, "Impure C# with Alpha Beta Pruning";
        public override string ToString()
        {
            return TicTacToeModel.ToString();
        }

        // Called when either the Computer or the human has chosen a move
        public void MakeMove(TicTacToeSquareModel square)
        {
            //  convert the square coordinates into a move object for the model
            var move = TicTacToeModel.CreateMove(square.row, square.col);
            // apply the move to the current game to get a new game state
            var newGame = TicTacToeModel.ApplyMove(gameState, move);
            // !! Hack to trick system into thinking that Stateful game states have actually changed
            gameState = default(Game); 
            // update the current game state to this new game state
            gameState = newGame;
        }

        private async Task<Move> ComputerThink()
        {
            // create a background task for the computer to think about it's move (so that the UI thread remains responsive)
            return await Task.Run(() => TicTacToeModel.FindBestMove(gameState));
        }

        // The times when the computer last started and finished thinking about it's best move
        private DateTime finishThinking, startThinking = default(DateTime);

        // Called when it's the computer's turn to make a move
        public async Task<TicTacToeSquareModel> ComputerMove()
        {
            System.Diagnostics.Debug.Assert(IsComputerTurn);

            startThinking = DateTime.Now;
            parent.Idle = false;

            // wait until the computer has finished deciding
            var move = await ComputerThink();

            finishThinking = DateTime.Now;
            parent.Idle = true;

            var square = parent.FindSquare(move.Row, move.Col);
            // apply the chosen move to update the state of the game
            MakeMove(square);

            return square;
        }

        // provides real-time update on how many nodes have been visited so far and time elapsed for current computer move
        public string DisplayProgress(bool done)
        {
            if (startThinking != default(DateTime))
            {
                int count = NodeCounter.Count;
                var seconds = ((done ? finishThinking : DateTime.Now) - startThinking).TotalSeconds;
                return (done ? "Finished: " : "") +
                    count.ToString("N0") + " nodes in " +
                    (done ? seconds.ToString("f3") : seconds.ToString("f0"))   + " seconds";
            }
            else
                return "";
        }

        // Called when the human has selected a square
        public void HumanMove(TicTacToeSquareModel square)
        {
            //System.Diagnostics.Debug.Assert(IsHumanTurn);
            MakeMove(square);
        }

        // Called when a new game is to be started
        public void StartNewGame(int size, bool humanFirst)
        {
            gameState = TicTacToeModel.GameStart(humanFirst ? Human : Computer, size);
            parent.Idle = humanFirst;
        }

        // update the state of the squares to reflect new changes to the game state
        private void RefreshSquares()
        {
            foreach (var square in parent.squares)
            {
                square.IsHumanTurn = this.IsHumanTurn;
                square.Piece = gameState.getPiece(square.row, square.col);
                if (square.IsHumanTurn)
                    square.HighLight = false;
            }
        }

        // called when the state of the game has changed in order to update the UI
        private async void RefreshView()
        {
            RefreshSquares();

            // determine if someone has one the game or if the game is over in a draw
            var outcome = TicTacToeModel.GameOutcome(gameState);

            if (outcome.IsWin)
            {
                // tell the user who won ...
                var win = outcome as TicTacToeOutcome<Player>.Win;
                if (win.winner.Equals(Computer))
                    parent.Message = "Bad luck, the computer beat you!";
                else
                    parent.Message = "Congratulations, you won! (there must be a bug in your code)";

                // highlight the winning line of squares
                foreach (var (row, col) in win.line)
                    parent.FindSquare(row, col).HighLight = true;
            }
            else if (outcome.IsUndecided)
                // tell the user who's turn it is ...
                if (gameState.Turn.Equals(Human))
                    parent.Message = "Your turn ...";
                else
                    parent.Message = "Please wait, the computer is thinking ...";
            else if (outcome.IsDraw)
                parent.Message = parent.Message = "It's a draw";

            // if it's now the computer's turn, start thinking about it's next move ...
            if (IsComputerTurn && parent.AutoStartComputerMove)
                await ComputerMove();
        }

        // Construct a new model specific child of the View Model
        public TicTacToeViewModelChild(TicTacToeViewModel parent, ITicTacToeModel<Game, Move, Player> TicTacToeModel)
        {
            this.parent = parent;
            this.TicTacToeModel = TicTacToeModel;

            // if the game state has changed and it's not null, update the UI to reflect the new game state
            this.WhenAnyValue(vm => vm.gameState)
                .Where(gameState => gameState != null)
                .Subscribe(gameState => RefreshView() );
        }
    }
}
