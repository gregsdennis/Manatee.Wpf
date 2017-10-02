using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manatee.Wpf
{
	/// <summary>
	/// A <see cref="ContentControl"/> that provides behavior for asynchronous loading and error handling.
	/// </summary>
	public class LoadableContent : ContentControl
	{
		/// <summary>
		/// Gets or sets whether the loading process has failed.
		/// </summary>
		public bool ShowError
		{
			get { return (bool)GetValue(ShowErrorProperty); }
			set { SetValue(ShowErrorProperty, value); }
		}
		public static readonly DependencyProperty ShowErrorProperty =
			DependencyProperty.Register("ShowError", typeof(bool), typeof(LoadableContent), new PropertyMetadata(false));
		
		/// <summary>
		/// Gets or sets the error message.
		/// </summary>
		public string ErrorMessage
		{
			get { return (string)GetValue(ErrorMessageProperty); }
			set { SetValue(ErrorMessageProperty, value); }
		}
		public static readonly DependencyProperty ErrorMessageProperty =
			DependencyProperty.Register("ErrorMessage", typeof(string), typeof(LoadableContent), new PropertyMetadata(null));

		/// <summary>
		/// Gets or sets the busy message.
		/// </summary>
		public string BusyMessage
		{
			get { return (string)GetValue(BusyMessageProperty); }
			set { SetValue(BusyMessageProperty, value); }
		}
		public static readonly DependencyProperty BusyMessageProperty =
			DependencyProperty.Register("BusyMessage", typeof(string), typeof(LoadableContent), new PropertyMetadata(null));

		/// <summary>
		/// Gets or sets whether the loading process is busy.
		/// </summary>
		public bool ShowBusy
		{
			get { return (bool)GetValue(ShowBusyProperty); }
			set { SetValue(ShowBusyProperty, value); }
		}
		public static readonly DependencyProperty ShowBusyProperty =
			DependencyProperty.Register("ShowBusy", typeof(bool), typeof(LoadableContent), new PropertyMetadata(false));

		/// <summary>
		/// Gets or sets the command to run when clicking the button shown in the error state.
		/// </summary>
		public ICommand ErrorButtonCommand
		{
			get { return (ICommand)GetValue(ErrorButtonCommandProperty); }
			set { SetValue(ErrorButtonCommandProperty, value); }
		}
		public static readonly DependencyProperty ErrorButtonCommandProperty =
			DependencyProperty.Register("ErrorButtonCommand", typeof(ICommand), typeof(LoadableContent), new PropertyMetadata(null));

		/// <summary>
		/// Gets or sets the content of the button shown in the error state.
		/// </summary>
		public object ErrorButtonContent
		{
			get { return (object)GetValue(ErrorButtonContentProperty); }
			set { SetValue(ErrorButtonContentProperty, value); }
		}
		public static readonly DependencyProperty ErrorButtonContentProperty =
			DependencyProperty.Register("ErrorButtonContent", typeof(object), typeof(LoadableContent), new PropertyMetadata(null));

		/// <summary>
		/// Gets or sets whether to show the button in the error state.
		/// </summary>
		public bool ShowErrorButton
		{
			get { return (bool)GetValue(ShowErrorButtonProperty); }
			set { SetValue(ShowErrorButtonProperty, value); }
		}
		public static readonly DependencyProperty ShowErrorButtonProperty =
			DependencyProperty.Register("ShowErrorButton", typeof(bool), typeof(LoadableContent), new PropertyMetadata(false));

		static LoadableContent()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(LoadableContent), new FrameworkPropertyMetadata(typeof(LoadableContent)));
		}
	}
}
