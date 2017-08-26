using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	/// <summary>
	/// Converts boolean values to and from <see cref="Visibility"/>.
	/// </summary>
	public class BoolToVisibilityConverter : IValueConverter
	{
		private static BoolToVisibilityConverter _trueToCollapsed;
		private static BoolToVisibilityConverter _trueToHidden;
		private static BoolToVisibilityConverter _falseToCollapsed;
		private static BoolToVisibilityConverter _falseToHidden;

		private readonly bool _isInverted;
		private readonly Visibility _nonVisibleState;

		/// <summary>
		/// Converts true to <see cref="Visibility.Collapsed"/> and
		/// false to <see cref="Visibility.Visible"/>.
		/// </summary>
		public static BoolToVisibilityConverter TrueToCollapsed =>
			_trueToCollapsed ?? (_trueToCollapsed = new BoolToVisibilityConverter(true, Visibility.Collapsed));
		/// <summary>
		/// Converts true to <see cref="Visibility.Hidden"/> and
		/// false to <see cref="Visibility.Visible"/>.
		/// </summary>
		public static BoolToVisibilityConverter TrueToHidden =>
			_trueToHidden ?? (_trueToHidden = new BoolToVisibilityConverter(true, Visibility.Hidden));
		/// <summary>
		/// Converts true to <see cref="Visibility.Visible"/> and
		/// false to <see cref="Visibility.Collapsed"/>.
		/// </summary>
		public static BoolToVisibilityConverter FalseToCollapsed =>
			_falseToCollapsed ?? (_falseToCollapsed = new BoolToVisibilityConverter(false, Visibility.Collapsed));
		/// <summary>
		/// Converts true to <see cref="Visibility.Visible"/> and
		/// false to <see cref="Visibility.Hidden"/>.
		/// </summary>
		public static BoolToVisibilityConverter FalseToHidden =>
			_falseToHidden ?? (_falseToHidden = new BoolToVisibilityConverter(false, Visibility.Hidden));

		private BoolToVisibilityConverter(bool isInverted, Visibility nonVisibleState)
		{
			_isInverted = isInverted;
			_nonVisibleState = nonVisibleState;
		}

		/// <summary>Converts a value. </summary>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool)) return value;

			return LogicInverter.InvertIfNecessary((bool) value, _isInverted, Visibility.Visible, _nonVisibleState);
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
