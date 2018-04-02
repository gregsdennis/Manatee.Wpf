namespace Manatee.Wpf.Forms.ViewModel.Validation
{
	public class StringMaxLengthRule : FieldValidationRuleBase<string>
	{
		public decimal Max { get; }
		public bool IsInclusive { get; }

		public StringMaxLengthRule(decimal max, bool isInclusive)
		{
			Max = max;
			IsInclusive = isInclusive;

			ErrorMessage = IsInclusive
				               ? $"The value may not be longer than {Max} characters."
				               : $"The value must be shorter than {Max} characters.";
		}

		protected override bool Passes(string value)
		{
			return IsInclusive
				       ? value?.Length <= Max
				       : value?.Length < Max;
		}
	}
}