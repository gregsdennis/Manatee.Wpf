using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manatee.Wpf
{
	/// <summary>
	/// Interaction logic for LoadableContent.xaml
	/// </summary>
	public partial class LoadableContent : UserControl
	{


		public bool ShowError
		{
			get { return (bool)GetValue(ShowErrorProperty); }
			set { SetValue(ShowErrorProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ShowError.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ShowErrorProperty =
			DependencyProperty.Register("ShowError", typeof(bool), typeof(LoadableContent), new PropertyMetadata(false));



		public string ErrorMessage
		{
			get { return (string)GetValue(ErrorMessageProperty); }
			set { SetValue(ErrorMessageProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ErrorMessage.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ErrorMessageProperty =
			DependencyProperty.Register("ErrorMessage", typeof(string), typeof(LoadableContent), new PropertyMetadata(null));



		public string BusyMessage
		{
			get { return (string)GetValue(BusyMessageProperty); }
			set { SetValue(BusyMessageProperty, value); }
		}

		// Using a DependencyProperty as the backing store for BusyMessage.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BusyMessageProperty =
			DependencyProperty.Register("BusyMessage", typeof(string), typeof(LoadableContent), new PropertyMetadata(null));



		public bool ShowBusy
		{
			get { return (bool)GetValue(ShowBusyProperty); }
			set { SetValue(ShowBusyProperty, value); }
		}

		// Using a DependencyProperty as the backing store for IsBusy.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ShowBusyProperty =
			DependencyProperty.Register("ShowBusy", typeof(bool), typeof(LoadableContent), new PropertyMetadata(false));



		public ICommand ErrorButtonCommand
		{
			get { return (ICommand)GetValue(ErrorButtonCommandProperty); }
			set { SetValue(ErrorButtonCommandProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ErrorButtonCommand.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ErrorButtonCommandProperty =
			DependencyProperty.Register("ErrorButtonCommand", typeof(ICommand), typeof(LoadableContent), new PropertyMetadata(null));



		public object ErrorButtonContent
		{
			get { return (object)GetValue(ErrorButtonContentProperty); }
			set { SetValue(ErrorButtonContentProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ErrorButtonContent.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ErrorButtonContentProperty =
			DependencyProperty.Register("ErrorButtonContent", typeof(object), typeof(LoadableContent), new PropertyMetadata(null));



		public bool ShowErrorButton
		{
			get { return (bool)GetValue(ShowErrorButtonProperty); }
			set { SetValue(ShowErrorButtonProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ShowErrorButton.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ShowErrorButtonProperty =
			DependencyProperty.Register("ShowErrorButton", typeof(bool), typeof(LoadableContent), new PropertyMetadata(false));



		public object CustomContent
		{
			get { return (object)GetValue(CustomContentProperty); }
			set { SetValue(CustomContentProperty, value); }
		}

		// Using a DependencyProperty as the backing store for LoadableContent.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty CustomContentProperty =
			DependencyProperty.Register("CustomContent", typeof(object), typeof(LoadableContent), new PropertyMetadata(null));



		public LoadableContent()
		{
			InitializeComponent();
		}
	}
}
