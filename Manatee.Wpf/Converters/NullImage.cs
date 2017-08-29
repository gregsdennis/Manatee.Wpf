﻿using System;
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
		/// <summary>
		/// Gets the default instance.
		/// </summary>
		public static NullImage Instance { get; }

		static NullImage()
		{
			Instance = new NullImage();
		}
		private NullImage() {}

		/// <summary>Converts a value. </summary>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value ?? DependencyProperty.UnsetValue;
		}
		/// <summary>Converts a value. </summary>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
