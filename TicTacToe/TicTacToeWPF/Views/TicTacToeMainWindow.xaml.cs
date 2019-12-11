using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Windows.Input;

namespace QUT
{
    public partial class TicTacToeMainWindow : ReactiveWindow<TicTacToeViewModel>
    {
        public TicTacToeMainWindow()
        {
            // Open main window in the centre of the screen
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();

            // Connect the view to it's view model
            this.ViewModel = new TicTacToeViewModel();

            this.WhenActivated(
                d =>
                {
                    // if computer is thinking, show waiting cursor
                    this.OneWayBind(this.ViewModel, vm => vm.Idle, view => view.Cursor, idle => idle ? Cursors.Arrow : Cursors.Wait);
                    // if computer is thinking, don't allow the game engine to be changed
                    this.OneWayBind(this.ViewModel, vm => vm.Idle, view => view.GameEngine.IsEnabled);
                    // if computer is thinking, don't allow a new game to be started
                    this.OneWayBind(this.ViewModel, vm => vm.Idle, view => view.Menu.IsEnabled);
                    // bind the squares in the view model to the corresponding user controls in the view
                    this.OneWayBind(this.ViewModel, vm => vm.squares, view => view.Board.ItemsSource);
                    // bind the list of game engines in the view model to the combo box in the view
                    this.OneWayBind(this.ViewModel, vm => vm.models, view => view.GameEngine.ItemsSource);
                    // bind the elapsed time (computer thinking) property in the view model to a label in the view
                    this.OneWayBind(this.ViewModel, vm => vm.TimeElapsed, view => view.TimeElapsed.Content);
                    // bind the game engine selected in the combo box to a property of the view model 
                    this.Bind(this.ViewModel, vm => vm.SelectedModel, view => view.GameEngine.SelectedValue);
                    // bind the message property of the view model to a label of the view
                    this.Bind(this.ViewModel, vm => vm.Message, view => view.MessageLabel.Content);
                    // bind the Human First checkbox in the view to a boolean property of the view model
                    this.Bind(this.ViewModel, vm => vm.HumanFirst, view => view.HumanFirst.IsChecked);
                    // bind the new 2x2 game menu item to a command of the view model
                    this.BindCommand(this.ViewModel, vm => vm.NewGameCommand, view => view.NewGame2x2, Observable.Return(2));
                    // bind the new 3x3 game menu item to a command of the view model
                    this.BindCommand(this.ViewModel, vm => vm.NewGameCommand, view => view.NewGame3x3, Observable.Return(3));
                    // bind the new 4x4 game menu item to a command of the view model
                    this.BindCommand(this.ViewModel, vm => vm.NewGameCommand, view => view.NewGame4x4, Observable.Return(4));
                });
        }
    }
}
