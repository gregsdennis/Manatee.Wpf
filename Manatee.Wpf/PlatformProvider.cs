using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Manatee.Wpf.ViewModel;

namespace Manatee.Wpf
{
	public class PlatformProvider : IPlatformProvider
	{
		private bool? _inDesignMode;

		private PlatformProvider() { }

		public static void Initialize()
		{
			if (ViewModel.PlatformProvider.Current == null)
				ViewModel.PlatformProvider.Current = new PlatformProvider();
		}

		public event EventHandler RequerySuggested
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool InDesignMode
		{
			get
			{
				if (_inDesignMode == null)
				{
					var descriptor = DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement));
					_inDesignMode = (bool)descriptor.Metadata.DefaultValue;
				}

				return _inDesignMode.GetValueOrDefault(false);
			}
		}

		public void OnUiThread(Action action)
		{
			if (Dispatcher.CurrentDispatcher.CheckAccess())
				action();
			else
				Dispatcher.CurrentDispatcher.Invoke(action);
		}

		public void InvalidateRequerySuggested()
		{
			CommandManager.InvalidateRequerySuggested();
		}

		public Task BeginOnUiThread(Action action)
		{
			return Dispatcher.CurrentDispatcher.InvokeAsync(action).Task;
		}
	}
}