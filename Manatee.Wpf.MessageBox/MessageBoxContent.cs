using System.Windows;
using System.Windows.Controls;

namespace Manatee.Wpf.MessageBox
{
	public class MessageBoxContent : Control
	{
		public double MessageFontSize
		{
			get { return (double)GetValue(MessageFontSizeProperty); }
			set { SetValue(MessageFontSizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for MessageFontSize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MessageFontSizeProperty =
			DependencyProperty.Register("MessageFontSize", typeof(double), typeof(MessageBoxContent), new PropertyMetadata(14.0));

		public double ButtonFontSize
		{
			get { return (double)GetValue(ButtonFontSizeProperty); }
			set { SetValue(ButtonFontSizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ButtonFontSize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ButtonFontSizeProperty =
			DependencyProperty.Register("ButtonFontSize", typeof(double), typeof(MessageBoxContent), new PropertyMetadata(12.0));

		public double IconSize
		{
			get { return (double)GetValue(IconSizeProperty); }
			set { SetValue(IconSizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for IconSize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IconSizeProperty =
			DependencyProperty.Register("IconSize", typeof(double), typeof(MessageBoxContent), new PropertyMetadata(50.0));

		static MessageBoxContent()
		{
			ViewModel.PlatformProvider.Current = PlatformProvider.Instance;


			DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBoxContent), new FrameworkPropertyMetadata(typeof(MessageBoxContent)));
		}
	}
}
