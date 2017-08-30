using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	public class Equality : IMultiValueConverter
	{
		public static Equality AreEqual { get; }
		public static Equality AreNotEqual { get; }

		private readonly bool _invert;

		static Equality()
		{
			AreEqual = new Equality(false);
			AreNotEqual = new Equality(true);
		}
		private Equality(bool invert)
		{
			_invert = invert;
		}

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var distinct = values.Distinct().Count();
			return _invert ? distinct != 1 : distinct == 1;
		}
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
