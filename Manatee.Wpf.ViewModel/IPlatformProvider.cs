using System;

namespace Manatee.Wpf.ViewModel
{
	public interface IPlatformProvider
	{
		event EventHandler RequerySuggested;

		void OnUiThread(Action action);
	}
}