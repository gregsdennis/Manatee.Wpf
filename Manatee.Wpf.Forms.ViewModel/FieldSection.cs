using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Manatee.Wpf.ViewModel;

namespace Manatee.Wpf.Forms.ViewModel
{
	public class FieldSection : ViewModelBase
	{
		private string _title;

		public string Title
		{
			get { return _title; }
			set
			{
				if (value == _title) return;
				_title = value;
				NotifyOfPropertyChange();
			}
		}

		public IList<Field> Fields { get; }
		public bool HasChanged => Fields.Any(f => f.HasChanged);
		public bool HasError => Fields.Any(f => f.HasError);

		public FieldSection()
		{
			Fields = new ObservableCollection<Field>();
		}
		public void Validate()
		{
			foreach (var field in Fields)
			{
				field.Validate();
			}
		}
	}
}