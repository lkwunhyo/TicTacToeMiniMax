using System.Collections.ObjectModel;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using System.Reactive;
using ReactiveUI.Fody.Helpers;
using System.Threading.Tasks;

namespace QUT
{
    // This interface is used to allow dependency injection of different implementations of the model 
    // (either C# or F#, both pure and impure and with/without alpha beta pruning)
    public interface ITicTacToeViewModelChild
    {
        bool IsHumanTurn { get; }
        bool IsComputerTurn { get; }
        bool IsGameOver { get; }
        void StartNewGame(int size, bool humanFirst);
        void HumanMove(TicTacToeSquareModel square);
        string DisplayProgress(bool done);
        Task<TicTacToeSquareModel> ComputerMove();
    }

    public class TicTacToeViewModel : ReactiveObject
    {
        // dependency injection of the part of the view model that depends on the game types in the model
        private ITicTacToeViewModelChild viewModelChild;

        // the command which is fired when the new game menu item is choosen (with size as a parameter)
        public ReactiveUI.ReactiveCommand<int, Unit> NewGameCommand { get; private set; }

        // for 3x3 game, this collection contains the 9 squares on the board
        public ObservableCollection<TicTacToeSquareModel> squares = new ObservableCollection<TicTacToeSquareModel>();

        // a list of the different model implementations (both C# and F#, pure and impure, with/without alpha beta pruning)
        public ObservableCollection<ITicTacToeViewModelChild> models = new ObservableCollection<ITicTacToeViewModelChild>();

        // which of these models is currently selected by the user
        [Reactive] public ITicTacToeViewModelChild SelectedModel { get; set; }

        // Derived property that reports the number of nodes visited and time taken for computer move
        readonly ObservableAsPropertyHelper<string> timeElapsed;
        public string TimeElapsed => timeElapsed.Value;

        // True iff the computer should make the first move of the game
        [Reactive] public bool HumanFirst { get; set; } = true;

        // True iff the computer is not busy thinking about it's next move
        [Reactive] public bool Idle { get; set; }

        // Message to tell the user who's turn it is and possibly the outcome of the game
        [Reactive] public string Message { get; set; }

        // Flag used when testing to prevent the computer starting to think about it's move as soon as the human has had a turn
        public bool AutoStartComputerMove { get; set; } = true;

        // find the square for the given row, column coordinates
        public TicTacToeSquareModel FindSquare(int row, int col)
        {
            foreach (var square in squares)
                if (square.row == row && square.col == col)
                    return square;

            throw new System.ArgumentOutOfRangeException();
        }

        // Derived property which defers to the model to test if the game is over
        public bool IsGameOver => viewModelChild != null && viewModelChild.IsGameOver;

        // Derived property which defers to the model to test if it is currently the human's turn to make a move
        public bool IsHumanTurn => viewModelChild != null && viewModelChild.IsHumanTurn;

        // Derived property which defers to the model to test if it is currently the computer's turn to make a move
        public bool IsComputerTurn => viewModelChild != null && viewModelChild.IsComputerTurn;

        // Called when it's time for the Computer to choose a move
        public Task<TicTacToeSquareModel> ComputerMove()
        {
            // delegate the the model specific part of view model
            return viewModelChild.ComputerMove();
        }

        // Called with the human has selected a square as their move
        public void HumanMove(TicTacToeSquareModel square)
        {
            // delegate the the model specific part of view model
            viewModelChild.HumanMove(square);
        }

        // remember what size game we last played (if the model is changed)
        private int lastSize = 3;

        // called when a new game is to be started
        public void StartNewGame(int size, bool humanFirst)
        {
            lastSize = size;
            squares.Clear();
            // the new game may be a different size, so recreate all squares and connect their select commands to the HumanMove method
            for (int row = 0; row < size; row++)
                for (int col = 0; col < size; col++)
                {
                    var square = new TicTacToeSquareModel(row, col);
                    square.SelectCommand.Subscribe(HumanMove);
                    squares.Add(square);
                }

            // create a new model for the new game
            viewModelChild.StartNewGame(size, humanFirst);
        }

        public TicTacToeViewModel()
        {
            // Update the progress every second while the computer is thinking about it's best move
            var whileWaiting =
                Observable.Interval(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
                .Where(time => this.IsComputerTurn)
                .Select(time => viewModelChild.DisplayProgress(false));

            // Update the progress when the computer has finished thinking about it's best move
            var whenFinished =
                this.WhenAnyValue(vm => vm.Idle)
                .Where(idle => idle)
                .Select(idle => viewModelChild.DisplayProgress(true));

            // Derived property to present to the user the computer's current progress towards finding a best move
            // Shows number of nodes visited and number of seconds elapsed
            var progressChanged = new[] { whileWaiting, whenFinished };
            this.timeElapsed = Observable.Merge(progressChanged).ToProperty(this, vm => vm.TimeElapsed);

            // add pure F# model using basic MiniMax algorithm 
            models.Add(new TicTacToeViewModelChild<FSharpPureTicTacToeModel.GameState, FSharpPureTicTacToeModel.Move, FSharpPureTicTacToeModel.Player>(this, new FSharpPureTicTacToeModel.BasicMiniMax()));
            // add pure F# model using MiniMax with alpha beta pruning
            models.Add(new TicTacToeViewModelChild<FSharpPureTicTacToeModel.GameState, FSharpPureTicTacToeModel.Move, FSharpPureTicTacToeModel.Player>(this, new FSharpPureTicTacToeModel.WithAlphaBetaPruning()));
            // add impure F# model using MiniMax with alpha beta pruning
            models.Add(new TicTacToeViewModelChild<FSharpImpureTicTacToeModel.GameState, FSharpImpureTicTacToeModel.Move, FSharpImpureTicTacToeModel.Player>(this, new FSharpImpureTicTacToeModel.WithAlphaBetaPruning()));
            // add impure C# model using MiniMax with alpha beta pruning
            models.Add(new TicTacToeViewModelChild<CSharpTicTacToe.Game, CSharpTicTacToe.Move, CSharpTicTacToe.Player>(this, new CSharpTicTacToe.WithAlphaBetaPruning()));

            // when the user selects a new model, change our child and start a new game
            this.WhenAnyValue(vm => vm.SelectedModel)
                .Where(child => child != null)
                .Subscribe(child => { this.viewModelChild = child; StartNewGame(lastSize, HumanFirst); });

            this.ThrownExceptions.Subscribe((x) => throw x);

            // choose the first model (pure F# using basic Minimax algorithm) by default
            SelectedModel = models[0];

            // create the command which is triggered when the user chooses a new game menu item
            NewGameCommand = ReactiveCommand.Create(new Action<int>(size => this.StartNewGame(size, HumanFirst)));
        }
    }
}