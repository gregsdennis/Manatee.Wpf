using System;
using System.Globalization;
using System.Windows.Controls;

namespace Manatee.Wpf.Tests.Client
{
	internal class NumericFormatExampleValidationRule : ValidationRule
	{
		// Ordinarily, we don't use exception handling for flow.
		// But for this example code, it's the easiest way to determine that
		// a format string is valid.
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			try
			{
				return new ValidationResult(true, 1.0.ToString(value as string));
			}
			catch (ArgumentNullException e)
			{
				return new ValidationResult(false, e.Message);
			}
			catch (FormatException e)
			{
				return new ValidationResult(false, e.Message);
			}
		}
	}
}