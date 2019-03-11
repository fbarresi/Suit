using System.Linq;
using System.Windows;
using Dragablz;
using Suit.Gui.ViewModels;
using Suit.Gui.Views;
using Suit.Interfaces.Commons;

namespace Suit.Gui
{
	public class InterTabClient : IInterTabClient
	{
		private readonly IViewModelFactory viewModelFactory;

		public InterTabClient(IViewModelFactory viewModelFactory)
		{
			this.viewModelFactory = viewModelFactory;
		}
		public INewTabHost<Window> GetNewHost(IInterTabClient interTabClient, object partition, TabablzControl source)
		{
			var viewModel = viewModelFactory.CreateViewModel<MainWindowViewModel>();
			var window = new MainWindow();
			window.DataContext = viewModel;
			return new NewTabHost<MainWindow>(window, window.TabablzControl);
		}

		public TabEmptiedResponse TabEmptiedHandler(TabablzControl tabControl, Window window)
		{
			return Application.Current.Windows.OfType<MainWindow>().Count() == 1
				? TabEmptiedResponse.DoNothing
				: TabEmptiedResponse.CloseWindowOrLayoutBranch;
		}
	}
}