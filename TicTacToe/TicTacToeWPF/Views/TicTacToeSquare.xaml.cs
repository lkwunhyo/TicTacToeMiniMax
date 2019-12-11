using ReactiveUI;
using System.Windows.Input;
using System.Windows.Media;

namespace QUT
{
    public partial class TicTacToeSquare : ReactiveUserControl<TicTacToeSquareModel>
    {
        private Brush defaultBrush;

        public TicTacToeSquare()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                defaultBrush = this.button.Background;
                // bind the Nought or Cross piece from the view model to be the text displayed on the square in the view
                this.OneWayBind(this.ViewModel, vm => vm.Piece, view => view.button.Content);
                // bind the Highlighted boolean property of the view model to the Background of the square in the view
                this.OneWayBind(this.ViewModel, vm => vm.HighLight, view => view.button.Background, this.BackGround);
                // bind the Mouse Down event of the square in the view to a command of the view model
                this.BindCommand(this.ViewModel, vm => vm.SelectCommand, view => view.button, nameof(button.MouseDown));
                // bind the IsEnabled property of the square in the view to a boolean property of the view model
                this.OneWayBind(this.ViewModel, vm => vm.IsEnabled, view => view.IsEnabled);

            });
        }
        private Brush BackGround(bool highlighted)
        {
            // Draw the background of the square as red if the square has been highlighted
            return highlighted ? Brushes.Red : defaultBrush;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            // Draw the square blue when the user does a mouse over
            if (button.Background != Brushes.Red)
                button.Background = Brushes.LightBlue;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            // revert to normal background colour when the mouse leaves the square
            if (button.Background != Brushes.Red)
                button.Background = defaultBrush;
        }
    }
}
