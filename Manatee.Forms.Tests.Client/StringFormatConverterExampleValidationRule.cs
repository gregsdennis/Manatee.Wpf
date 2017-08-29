using System;
using System.Globalization;
using System.Windows.Controls;

namespace Manatee.Wpf.Tests.Client
{
	internal class StringFormatConverterExampleValidationRule : ValidationRule
	{
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			try
			{
				return new ValidationResult(true, string.Format(value as string, 1, 2, 3));
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