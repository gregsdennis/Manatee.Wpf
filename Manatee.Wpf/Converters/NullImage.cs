using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	/// <summary>
	/// Protects against image source binding errors when the source is null.
	/// </summary>
	public class NullImage : IValueConverter
	{
		public static NullImage Instance { get; }

		static NullImage()
		{
			Instance = new NullImage();
		}
		private NullImage() {}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value ?? DependencyProperty.UnsetValue;
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
