using System;
using System.Threading.Tasks;

namespace Manatee.Wpf.ViewModel
{
	public interface IPlatformProvider
	{
		event EventHandler RequerySuggested;
	    bool InDesignMode { get; }

	    void OnUiThread(Action action);
	    void InvalidateRequerySuggested();
	    Task BeginOnUiThread(Action action);
	}
}