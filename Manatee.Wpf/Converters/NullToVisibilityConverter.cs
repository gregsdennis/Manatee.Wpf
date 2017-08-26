using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	/// <summary>
	/// Converts nullness to and from <see cref="Visibility"/>.
	/// </summary>
	public class NullToVisibilityConverter : IValueConverter
	{
		private static NullToVisibilityConverter _notNullToCollapsed;
		private static NullToVisibilityConverter _notNullToHidden;
		private static NullToVisibilityConverter _nullToCollapsed;
		private static NullToVisibilityConverter _nullToHidden;

		private readonly bool _isInverted;
		private readonly Visibility _nonVisibleState;

		/// <summary>
		/// Converts true to <see cref="Visibility.Collapsed"/> and
		/// false to <see cref="Visibility.Visible"/>.
		/// </summary>
		public static NullToVisibilityConverter NotNullToCollapsed =>
			_notNullToCollapsed ?? (_notNullToCollapsed = new NullToVisibilityConverter(true, Visibility.Collapsed));
		/// <summary>
		/// Converts true to <see cref="Visibility.Hidden"/> and
		/// false to <see cref="Visibility.Visible"/>.
		/// </summary>
		public static NullToVisibilityConverter NotNullToHidden =>
			_notNullToHidden ?? (_notNullToHidden = new NullToVisibilityConverter(true, Visibility.Hidden));
		/// <summary>
		/// Converts true to <see cref="Visibility.Visible"/> and
		/// false to <see cref="Visibility.Collapsed"/>.
		/// </summary>
		public static NullToVisibilityConverter NullToCollapsed =>
			_nullToCollapsed ?? (_nullToCollapsed = new NullToVisibilityConverter(false, Visibility.Collapsed));
		/// <summary>
		/// Converts true to <see cref="Visibility.Visible"/> and
		/// false to <see cref="Visibility.Hidden"/>.
		/// </summary>
		public static NullToVisibilityConverter NullToHidden =>
			_nullToHidden ?? (_nullToHidden = new NullToVisibilityConverter(false, Visibility.Hidden));

		private NullToVisibilityConverter(bool isInverted, Visibility nonVisibleState)
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
			return LogicInverter.InvertIfNecessary(value != null, _isInverted, Visibility.Visible, _nonVisibleState);
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
