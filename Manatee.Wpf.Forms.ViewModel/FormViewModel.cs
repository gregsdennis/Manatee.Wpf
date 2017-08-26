using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Manatee.Wpf.Forms.ViewModel
{
	public class FormViewModel : ViewModelBase
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

		public string AcceptText { get; set; }
		public string CancelText { get; set; }
		public bool ShowCancel { get; set; }

		public IList<FieldSection> Sections { get; }

		public ICommand Accept { get; }
		public ICommand Cancel { get; }

		public event EventHandler AcceptRequested;
		public event EventHandler CancelRequested;

		public FormViewModel()
		{
			Sections = new ObservableCollection<FieldSection>();
			Accept = new RelayCommand(() => RaiseEvent(AcceptRequested), () => !Sections.Any(s => s.HasChanged && s.HasError));
			AcceptText = "Save";
			Cancel = new RelayCommand(() => RaiseEvent(CancelRequested));
			CancelText = "Cancel";
			ShowCancel = true;
		}

		public void Validate()
		{
			foreach (var section in Sections)
			{
				section.Validate();
			}
		}
	}
}
