using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	public class ValueToResourceConverter : IValueConverter
	{
		public static ValueToResourceConverter Instance { get; }

		static ValueToResourceConverter()
		{
			Instance = new ValueToResourceConverter();
		}
		private ValueToResourceConverter() {}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Application.Current.FindResource(value);
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
