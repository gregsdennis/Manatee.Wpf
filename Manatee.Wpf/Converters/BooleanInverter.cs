﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	/// <summary>
	/// Inverts a boolean value;
	/// </summary>
	public class BooleanInverter : IValueConverter
	{
		/// <summary>
		/// Gets the default instance.
		/// </summary>
		public static BooleanInverter Instance { get; }

		static BooleanInverter()
		{
			Instance = new BooleanInverter();
		}
		private BooleanInverter() {}

		/// <summary>Converts a value. </summary>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool)) throw new ArgumentException($"value must be of type '{typeof(bool)}'");

			return !(bool) value;
		}
		/// <summary>Converts a value. </summary>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool)) throw new ArgumentException($"value must be of type '{typeof(bool)}'");

			return !(bool)value;
		}
	}
}