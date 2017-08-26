using System;

namespace Manatee.Wpf.Forms.ViewModel.Validation
{
	public class RequiredValueRule<T> : FieldValidationRuleBase<T?>
		where T : struct
	{
		protected override bool Passes(T? value)
		{
			return value != null;
		}
	}
}