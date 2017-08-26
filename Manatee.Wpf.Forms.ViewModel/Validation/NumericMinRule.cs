namespace Manatee.Wpf.Forms.ViewModel.Validation
{
	public class NumericMinRule : FieldValidationRuleBase<decimal?>
	{
		public decimal Min { get; }
		public bool IsInclusive { get; }

		public NumericMinRule(decimal min, bool isInclusive)
		{
			Min = min;
			IsInclusive = isInclusive;

			ErrorMessage = IsInclusive
				               ? $"The value may not be less than {Min}."
				               : $"The value must be greater than {Min}.";
		}

		protected override bool Passes(decimal? value)
		{
			return IsInclusive
				       ? value >= Min
				       : value > Min;
		}
	}
}