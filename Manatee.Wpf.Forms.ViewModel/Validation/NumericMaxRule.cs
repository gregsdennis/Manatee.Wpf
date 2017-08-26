namespace Manatee.Wpf.Forms.ViewModel.Validation
{
	public class NumericMaxRule : FieldValidationRuleBase<decimal?>
	{
		public decimal Max { get; }
		public bool IsInclusive { get; }

		public NumericMaxRule(decimal max, bool isInclusive)
		{
			Max = max;
			IsInclusive = isInclusive;

			ErrorMessage = IsInclusive
				               ? $"The value may not be greater than {Max}."
				               : $"The value must be less than {Max}.";
		}

		protected override bool Passes(decimal? value)
		{
			return IsInclusive
				       ? value <= Max
				       : value < Max;
		}
	}
}
