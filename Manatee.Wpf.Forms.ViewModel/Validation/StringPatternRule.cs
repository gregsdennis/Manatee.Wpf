using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Manatee.Wpf.Forms.ViewModel.Validation
{
	public class StringPatternRule : FieldValidationRuleBase<string>
	{
		public Regex Pattern { get; }

		public StringPatternRule([RegexPattern] string pattern)
			: this(new Regex(pattern)) { }
		public StringPatternRule(Regex pattern)
		{
			Pattern = pattern;

			ErrorMessage = "The value does not match the required pattern.";
		}

		protected override bool Passes(string value)
		{
			return Pattern.IsMatch(value);
		}
	}
}