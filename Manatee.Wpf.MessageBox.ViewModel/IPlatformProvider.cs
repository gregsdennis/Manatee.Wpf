using System;

namespace Manatee.Wpf.MessageBox.ViewModel
{
	public interface IPlatformProvider
	{
		event EventHandler RequerySuggested;

		void OnUiThread(Action action);
	}
}