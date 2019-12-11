using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System.Reactive;

namespace QUT
{
    public class TicTacToeSquareModel: ReactiveUI.ReactiveObject
    {
        // "X" for Cross, "O" for Nought and "" for empty
        [Reactive] public string Piece { get; set; } = ""; 
        
        // need to know if it's the human's turn (as otherwise the square should not be enabled)
        [Reactive] public bool IsHumanTurn { get; set; }

        // used to highlight the line of squares that produced the win (if any)
        [Reactive] public bool HighLight { get; set; } = false;

        // Derived property which which determines if the square should be selectable
        private readonly ObservableAsPropertyHelper<bool> isEnabled;
        public bool IsEnabled => isEnabled.Value;

        // the row, column coordinates of the square 
        public int row { get; private set; }
        public int col { get; private set; }

        public TicTacToeSquareModel(int row, int col)
        {
            this.row = row;
            this.col = col;

            // Create the command which is fired when the user clicks on this square
            SelectCommand = ReactiveCommand.Create<Unit, TicTacToeSquareModel>(i => this);

            // compute derived property IsEnabled which is true iff the square is empty and it's the human's turn
            isEnabled = this
                .WhenAnyValue(square => square.Piece, square => square.IsHumanTurn, (piece, isHumanTurn) => (piece.Length == 0) && isHumanTurn)
                .ToProperty(this, square => square.IsEnabled);
        }

        // the command which is fired when the user clicks on this square
        public ReactiveUI.ReactiveCommand<Unit, TicTacToeSquareModel> SelectCommand { get; private set; }
    }
}