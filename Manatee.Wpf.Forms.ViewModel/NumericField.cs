namespace Manatee.Wpf.Forms.ViewModel
{
	public class NumericField : Field<decimal?>
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
		public NumericField(decimal? initialValue)
			: base(initialValue)
		{
			StringFormat = "G";
		}
	}
}