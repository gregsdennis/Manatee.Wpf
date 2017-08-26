using System;
using System.Windows.Input;
using System.Windows.Threading;
using Manatee.Wpf.MessageBox.ViewModel;

namespace Manatee.Wpf.MessageBox
{
	internal class PlatformProvider : IPlatformProvider
	{
		public static PlatformProvider Instance { get; }

		static PlatformProvider()
		{
			Instance = new PlatformProvider();
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