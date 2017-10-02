using System;
using System.Windows.Input;
using System.Windows.Threading;
using Manatee.Wpf.ViewModel;

namespace Manatee.Wpf.MessageBox
{
	internal class PlatformProvider : IPlatformProvider
	{
		public static void Initialize()
		{
			if (Wpf.ViewModel.PlatformProvider.Current == null)
				Wpf.ViewModel.PlatformProvider.Current = new PlatformProvider();
		}
		private PlatformProvider() { }

		public event EventHandler RequerySuggested
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void OnUiThread(Action action)
		{
			if (Dispatcher.CurrentDispatcher.CheckAccess())
				action();
			else
				Dispatcher.CurrentDispatcher.Invoke(action);
		}
	}
}