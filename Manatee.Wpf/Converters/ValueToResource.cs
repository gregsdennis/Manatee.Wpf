using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	public class ValueToResource : IValueConverter
	{
		public static ValueToResource Instance { get; }

		static ValueToResource()
		{
			Instance = new ValueToResource();
		}
		private ValueToResource() {}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return null;

			return Application.Current.FindResource(value);
		}
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
