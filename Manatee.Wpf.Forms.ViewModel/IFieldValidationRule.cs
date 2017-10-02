namespace Manatee.Wpf.Forms.ViewModel
{
	public interface IFieldValidationRule<in T>
	{
		string Validate(T value);
	}
}