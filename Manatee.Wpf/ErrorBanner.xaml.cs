using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Manatee.Wpf
{
	/// <summary>
	/// Interaction logic for ErrorBanner.xaml
	/// </summary>
	public partial class ErrorBanner : UserControl
	{
		public string ErrorMessage
		{
			get { return (string)GetValue(ErrorMessageProperty); }
			set { SetValue(ErrorMessageProperty, value); }
		}

		public static readonly DependencyProperty ErrorMessageProperty =
			DependencyProperty.Register("ErrorMessage", typeof(string), typeof(ErrorBanner), new PropertyMetadata(null));

		public object ButtonContent
		{
			get { return (object)GetValue(ButtonContentProperty); }
			set { SetValue(ButtonContentProperty, value); }
		}

		public static readonly DependencyProperty ButtonContentProperty =
			DependencyProperty.Register("ButtonContent", typeof(object), typeof(ErrorBanner), new PropertyMetadata(null));

		public bool ShowButton
		{
			get { return (bool)GetValue(ShowButtonProperty); }
			set { SetValue(ShowButtonProperty, value); }
		}

		public static readonly DependencyProperty ShowButtonProperty =
			DependencyProperty.Register("ShowButton", typeof(bool), typeof(ErrorBanner), new PropertyMetadata(false));

		public ICommand ButtonCommand
		{
			get { return (ICommand)GetValue(ButtonCommandProperty); }
			set { SetValue(ButtonCommandProperty, value); }
		}

		public static readonly DependencyProperty ButtonCommandProperty =
			DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(ErrorBanner), new PropertyMetadata(null));

		public Brush IconBrush
		{
			get { return (Brush)GetValue(IconBrushProperty); }
			set { SetValue(IconBrushProperty, value); }
		}

		public static readonly DependencyProperty IconBrushProperty =
			DependencyProperty.Register("IconBrush", typeof(Brush), typeof(ErrorBanner), new PropertyMetadata(Brushes.Red));

		public ErrorBanner()
		{
			InitializeComponent();
		}
	}
}
