namespace Manatee.Wpf.Forms.ViewModel
{
	public class ToggleField : Field<bool?>
	{
		public ToggleField() { }
		public ToggleField(bool? initialValue)
			: base(initialValue) { }
	}
}