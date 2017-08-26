using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Manatee.Wpf.Converters
{
	/// <summary>
	/// Converts string values to <see cref="Visibility"/>.
	/// </summary>
	public class StringToVisibilityConverter : IValueConverter
	{
		private static StringToVisibilityConverter _nullToCollapsed;
		private static StringToVisibilityConverter _nullToHidden;
		private static StringToVisibilityConverter _whitespaceToCollapsed;
		private static StringToVisibilityConverter _whitespaceToHidden;
		private static StringToVisibilityConverter _notNullToCollapsed;
		private static StringToVisibilityConverter _notNullToHidden;
		private static StringToVisibilityConverter _notWhitespaceToCollapsed;
		private static StringToVisibilityConverter _notWhitespaceToHidden;

		private readonly bool _isInverted;
		private readonly Visibility _nonVisibleState;
		private readonly Func<string, bool> _stringTest;

		/// <summary>
		/// Converts null to <see cref="Visibility.Collapsed"/> and
		/// non-null to <see cref="Visibility.Visible"/>.
		/// </summary>
		public static StringToVisibilityConverter NullToCollapsed =>
			_nullToCollapsed ?? (_nullToCollapsed = new StringToVisibilityConverter(true, Visibility.Collapsed, s => s == null));
		/// <summary>
		/// Converts null to <see cref="Visibility.Hidden"/> and
		/// non-null to <see cref="Visibility.Visible"/>.
		/// </summary>
		public static StringToVisibilityConverter NullToHidden =>
			_nullToHidden ?? (_nullToHidden = new StringToVisibilityConverter(true, Visibility.Hidden, s => s == null));
		/// <summary>
		/// Converts null or whitespace to <see cref="Visibility.Collapsed"/> and
		/// non-null, non-whitespace to <see cref="Visibility.Visible"/>.
		/// </summary>
		public static StringToVisibilityConverter WhitespaceToCollapsed =>
			_whitespaceToCollapsed ?? (_whitespaceToCollapsed = new StringToVisibilityConverter(true, Visibility.Collapsed, string.IsNullOrWhiteSpace));
		/// <summary>
		/// Converts null or whitespace to <see cref="Visibility.Hidden"/> and
		/// non-null to <see cref="Visibility.Visible"/>.
		/// </summary>
		public static StringToVisibilityConverter WhitespaceToHidden =>
			_whitespaceToHidden ?? (_whitespaceToHidden = new StringToVisibilityConverter(true, Visibility.Hidden, string.IsNullOrWhiteSpace));
		/// <summary>
		/// Converts null to <see cref="Visibility.Visible"/> and
		/// non-null to <see cref="Visibility.Collapsed"/>.
		/// </summary>
		public static StringToVisibilityConverter NotNullToCollapsed =>
			_notNullToCollapsed ?? (_notNullToCollapsed = new StringToVisibilityConverter(false, Visibility.Collapsed, s => s == null));
		/// <summary>
		/// Converts null to <see cref="Visibility.Visible"/> and
		/// non-null to <see cref="Visibility.Hidden"/>.
		/// </summary>
		public static StringToVisibilityConverter NotNullToHidden =>
			_notNullToHidden ?? (_notNullToHidden = new StringToVisibilityConverter(false, Visibility.Hidden, s => s == null));
		/// <summary>
		/// Converts null or whitespace to <see cref="Visibility.Visible"/> and
		/// non-null, non-whitespace to <see cref="Visibility.Collapsed"/>.
		/// </summary>
		public static StringToVisibilityConverter NotWhitespaceToCollapsed =>
			_notWhitespaceToCollapsed ?? (_notWhitespaceToCollapsed = new StringToVisibilityConverter(false, Visibility.Collapsed, string.IsNullOrWhiteSpace));
		/// <summary>
		/// Converts null or whitespace to <see cref="Visibility.Visible"/> and
		/// non-null, non-whitespace to <see cref="Visibility.Hidden"/>.
		/// </summary>
		public static StringToVisibilityConverter NotWhitespaceToHidden =>
			_notWhitespaceToHidden ?? (_notWhitespaceToHidden = new StringToVisibilityConverter(false, Visibility.Hidden, string.IsNullOrWhiteSpace));

		private StringToVisibilityConverter(bool isInverted, Visibility nonVisibleState, Func<string, bool> stringTest)
		{
			_isInverted = isInverted;
			_nonVisibleState = nonVisibleState;
			_stringTest = stringTest;
		}


		/// <summary>Converts a value. </summary>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null && !(value is string)) throw new ArgumentException($"value must be of type '{typeof(string)}'");

			return LogicInverter.InvertIfNecessary(_stringTest((string) value), _isInverted, Visibility.Visible, _nonVisibleState);
		}
		/// <summary>Converts a value. </summary>
		/// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("Cannot implement visibility -> string conversion.");
		}
	}
}
