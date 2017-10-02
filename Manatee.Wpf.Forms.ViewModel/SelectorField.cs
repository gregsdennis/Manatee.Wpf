using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Manatee.Wpf.Forms.ViewModel
{
	public class SelectorField<T> : Field<T>
	{
		public IList<T> Options { get; }

		public SelectorField()
			: this(default(T)) { }
		public SelectorField(T initialValue)
			: base(initialValue)
		{
			Options = new ObservableCollection<T>();
		}
	}
}