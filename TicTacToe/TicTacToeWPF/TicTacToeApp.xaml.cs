using System.Windows;
using ReactiveUI;
using Splat;
using System.Reflection;

namespace QUT
{
    public partial class TicTacToeApp : Application
    {
        public TicTacToeApp()
        {
            // Automatically associate each view with it's corresponding view model type
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }
    }
}
