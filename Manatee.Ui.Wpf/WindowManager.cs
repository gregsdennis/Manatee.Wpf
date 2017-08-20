using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Manatee.Ui.Mvvmc;

namespace Manatee.Ui.Wpf
{
	/// <summary>
	/// A service that manages windows.
	/// </summary>
	public class WindowManager : IWindowManager
	{
		private class WindowConductor
		{
			private bool _deactivatingFromView;
			private bool _deactivateFromViewModel;
			private bool _actuallyClosing;
			private readonly Window _view;
			private readonly IScreen _model;

			public WindowConductor(IScreen model, Window view)
			{
				_model = model;
				_view = view;

				view.Closed += _Closed;
				_model.Deactivated += _Deactivated;
				view.Closing += _Closing;

				_model.Activate()
				           .ContinueWith(r => this.Log().Warn("Could not activate model {0}", model));
			}

			private void _Closed(object sender, EventArgs e)
			{
				_view.Closed -= _Closed;
				_view.Closing -= _Closing;

				if (_deactivateFromViewModel) return;

				_deactivatingFromView = true;
				_model.Deactivate(true);
				_deactivatingFromView = false;
			}

			private void _Deactivated(object sender, DeactivationEventArgs e)
			{
				if (!e.WasClosed) return;

				_model.Deactivated -= _Deactivated;

				if (_deactivatingFromView) return;

				_deactivateFromViewModel = true;
				_actuallyClosing = true;
				Execute.OnUiThread(() => _view.Close());
				_actuallyClosing = false;
				_deactivateFromViewModel = false;
			}

			private void _Closing(object sender, CancelEventArgs e)
			{
				if (e.Cancel) return;

				if (_actuallyClosing)
				{
					_actuallyClosing = false;
					return;
				}

				bool runningAsync = false, shouldEnd = false;

				Execute.OnUiThread(async () =>
					{
						var canClose = await _model.CanClose();
						if (runningAsync && canClose)
						{
							_actuallyClosing = true;
							_view.Close();
						}
						else
							e.Cancel = !canClose;

						shouldEnd = true;
					});

				if (shouldEnd) return;

				runningAsync = e.Cancel = true;
			}
		}

		/// <summary>
		/// Shows a modal dialog for the specified model.
		/// </summary>
		/// <param name="rootModel">The root model.</param>
		/// <returns>The dialog result.</returns>
		public virtual void ShowDialog(IScreen rootModel)
		{
			Execute.OnUiThread(() => _CreateWindow(rootModel, true).ShowDialog());
		}

		/// <summary>
		/// Shows a window for the specified model.
		/// </summary>
		/// <param name="rootModel">The root model.</param>
		public virtual void ShowWindow(IScreen rootModel)
		{
			NavigationWindow navWindow = null;

			var application = Application.Current;
			if (application?.MainWindow != null)
				navWindow = application.MainWindow as NavigationWindow;

			if (navWindow != null)
			{
				var window = _CreatePage(rootModel);
				navWindow.Navigate(window);
			}
			else
				Execute.OnUiThread(() => _CreateWindow(rootModel, false).Show());
		}

		/// <summary>
		/// Creates a window.
		/// </summary>
		/// <param name="rootModel">The view model.</param>
		/// <param name="isDialog">Whethor or not the window is being shown as a dialog.</param>
		/// <returns>The window.</returns>
		private static Window _CreateWindow(IScreen rootModel, bool isDialog)
		{
			var view = _EnsureWindow(rootModel, ViewLocator.LocateForModel(rootModel, null), isDialog);

			new WindowConductor(rootModel, view);

			return view;
		}

		/// <summary>
		/// Makes sure the view is a window is is wrapped by one.
		/// </summary>
		/// <param name="model">The view model.</param>
		/// <param name="view">The view.</param>
		/// <param name="isDialog">Whethor or not the window is being shown as a dialog.</param>
		/// <returns>The window.</returns>
		private static Window _EnsureWindow(object model, object view, bool isDialog)
		{
			var window = view as Window;

			if (window == null)
			{
				window = new Window
					{
						Content = view,
						SizeToContent = SizeToContent.WidthAndHeight,
						Title = model.GetType().Name
					};

				var owner = _InferOwnerOf(window);
				if (owner != null)
				{
					window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
					window.Owner = owner;
				}
				else
					window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			}
			else
			{
				var owner = _InferOwnerOf(window);
				if (owner != null && isDialog)
					window.Owner = owner;
			}

			window.DataContext = model;

			return window;
		}

		/// <summary>
		/// Infers the owner of the window.
		/// </summary>
		/// <param name="window">The window to whose owner needs to be determined.</param>
		/// <returns>The owner.</returns>
		private static Window _InferOwnerOf(Window window)
		{
			var application = Application.Current;
			if (application == null) return null;

			var active = application.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
			active = active ?? (PresentationSource.FromVisual(application.MainWindow) == null ? null : application.MainWindow);
			return Equals(active, window) ? null : active;
		}

		/// <summary>
		/// Creates the page.
		/// </summary>
		/// <param name="rootModel">The root model.</param>
		/// <returns>The page.</returns>
		private static Page _CreatePage(object rootModel)
		{
			var view = _EnsurePage(rootModel, ViewLocator.LocateForModel(rootModel, null));

			var activatable = rootModel as IActivate;
			activatable?.Activate();

			var deactivatable = rootModel as IDeactivate;
			if (deactivatable != null)
				view.Unloaded += (s, e) => deactivatable.Deactivate(true);

			return view;
		}

		/// <summary>
		/// Ensures the view is a page or provides one.
		/// </summary>
		/// <param name="model">The model.</param>
		/// <param name="view">The view.</param>
		/// <returns>The page.</returns>
		private static Page _EnsurePage(object model, object view)
		{
			var page = view as Page ?? new Page {Content = view};

			page.DataContext = model;

			return page;
		}
	}
}