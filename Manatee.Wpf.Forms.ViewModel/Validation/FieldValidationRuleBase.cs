namespace Manatee.Wpf.Forms.ViewModel.Validation
{
	public abstract class FieldValidationRuleBase<T> : IFieldValidationRule<T>
	{
		public string ErrorMessage { get; set; }

		public string Validate(T value)
		{
			if (!Passes(value))
				return ErrorMessage;
			return null;
		}

		protected abstract bool Passes(T value);
	}
}