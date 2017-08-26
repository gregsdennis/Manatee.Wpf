using System;

namespace Manatee.Wpf.Forms.ViewModel
{
	public interface IPlatformProvider
	{
		event EventHandler RequerySuggested;

		void OnUiThread(Action action);
	}
}