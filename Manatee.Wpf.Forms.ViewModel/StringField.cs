namespace Manatee.Wpf.Forms.ViewModel
{
	public class StringField : Field<string>
	{
		public StringField() { }
		public StringField(string initialValue)
			: base(initialValue) { }

		protected override bool AreEqual(string value, string initialValue)
		{
			var left = value ?? string.Empty;
			var right = initialValue ?? string.Empty;

			return base.AreEqual(left, right);
		}
	}
}