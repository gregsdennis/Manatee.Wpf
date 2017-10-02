using System;

namespace Manatee.Wpf.Forms.ViewModel
{
	public class DateField : Field<DateTime?>
	{
		public DateField()
			: this(null) { }
		public DateField(DateTime? initialValue)
			: base(initialValue) { }
	}
}