using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	/// <summary>
	/// Protects against image source binding errors when the source is null.
	/// </summary>
	public class NullImageConverter : IValueConverter
	{
		public static NullImageConverter Instance { get; }

		static NullImageConverter()
		{
			Instance = new NullImageConverter();
		}
		private NullImageConverter() {}

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
