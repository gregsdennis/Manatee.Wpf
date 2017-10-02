namespace Manatee.Wpf.Forms.ViewModel
{
	public class NumericField : Field<double?>
	{
		private string _stringFormat;

		public string StringFormat
		{
			get { return _stringFormat; }
			set
			{
				if (value == _stringFormat) return;
				_stringFormat = value;
				NotifyOfPropertyChange();
			}
		}

		public NumericField()
			: this(null) { }
		public NumericField(double? initialValue)
			: base(initialValue)
		{
			StringFormat = "G";
		}
	}
}