namespace Manatee.Wpf.Forms.ViewModel.Validation
{
	public class StringMinLengthRule : FieldValidationRuleBase<string>
	{
		public decimal Min { get; }
		public bool IsInclusive { get; }

		public StringMinLengthRule(decimal min, bool isInclusive)
		{
			Min = min;
			IsInclusive = isInclusive;

			ErrorMessage = IsInclusive
				               ? $"The value may not be shorter than {Min} characters."
				               : $"The value must be longer than {Min} characters.";
		}

		protected override bool Passes(string value)
		{
			return IsInclusive
				       ? value.Length >= Min
				       : value.Length > Min;
		}
	}
}