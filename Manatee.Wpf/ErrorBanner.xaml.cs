using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;

namespace Manatee.Wpf
{
	/// <summary>
	/// Interaction logic for ErrorBanner.xaml
	/// </summary>
	public partial class ErrorBanner : UserControl
	{
		/// <summary>
		/// Gets or sets the error message.
		/// </summary>
		public string ErrorMessage
		{
			get { return (string) GetValue(ErrorMessageProperty); }
			set { SetValue(ErrorMessageProperty, value); }
		}
		/// <summary>
		/// Provides a backing field for <see cref="ErrorMessage"/>.
		/// </summary>
		public static readonly DependencyProperty ErrorMessageProperty =
			DependencyProperty.Register("ErrorMessage", typeof(string), typeof(ErrorBanner), new PropertyMetadata(null));

		/// <summary>
		/// Gets or sets the button content.
		/// </summary>
		public object ButtonContent
		{
			get { return (object) GetValue(ButtonContentProperty); }
			set { SetValue(ButtonContentProperty, value); }
		}
		/// <summary>
		/// Provides a backing field for <see cref="ButtonContent"/>.
		/// </summary>
		public static readonly DependencyProperty ButtonContentProperty =
			DependencyProperty.Register("ButtonContent", typeof(object), typeof(ErrorBanner), new PropertyMetadata(null));

		/// <summary>
		/// Gets or sets whether the button should be shown.
		/// </summary>
		public bool ShowButton
		{
			get { return (bool) GetValue(ShowButtonProperty); }
			set { SetValue(ShowButtonProperty, value); }
		}
		/// <summary>
		/// Provides a backing field for <see cref="ShowButton"/>.
		/// </summary>
		public static readonly DependencyProperty ShowButtonProperty =
			DependencyProperty.Register("ShowButton", typeof(bool), typeof(ErrorBanner), new PropertyMetadata(false));

		/// <summary>
		/// Gets or sets the command to be run by the button.
		/// </summary>
		public ICommand ButtonCommand
		{
			get { return (ICommand) GetValue(ButtonCommandProperty); }
			set { SetValue(ButtonCommandProperty, value); }
		}
		/// <summary>
		/// Provides a backing field for <see cref="ButtonCommand"/>.
		/// </summary>
		public static readonly DependencyProperty ButtonCommandProperty =
			DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(ErrorBanner), new PropertyMetadata(null));

		/// <summary>
		/// Gets or sets the brush for the icon.
		/// </summary>
		public Brush IconBrush
		{
			get { return (Brush) GetValue(IconBrushProperty); }
			set { SetValue(IconBrushProperty, value); }
		}
		/// <summary>
		/// Provides a backing field for <see cref="IconBrush"/>.
		/// </summary>
		public static readonly DependencyProperty IconBrushProperty =
			DependencyProperty.Register("IconBrush", typeof(Brush), typeof(ErrorBanner), new PropertyMetadata(Brushes.Red));

		/// <summary>
		/// Gets or sets the icon to display in the error state.
		/// </summary>
		public FontAwesomeIcon? Icon
		{
			get { return (FontAwesomeIcon?) GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
		/// <summary>
		/// Provides a backing field for <see cref="Icon"/>.
		/// </summary>
		public static readonly DependencyProperty IconProperty =
			DependencyProperty.Register("Icon", typeof(FontAwesomeIcon?), typeof(ErrorBanner), new PropertyMetadata(FontAwesomeIcon.ExclamationTriangle));



		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		public ErrorBanner()
		{
			InitializeComponent();
		}
	}
}
