using System.Collections.Generic;
using System.Linq;

namespace Manatee.Wpf.Forms.ViewModel
{
	public abstract class Field : ViewModelBase
	{
		private bool _hasChanged;
		private bool _hasError;
		private IEnumerable<string> _errorMessages;
		private string _label;
		private bool _requiresChange;

		public bool HasChanged
		{
			get { return _hasChanged; }
			protected set
			{
				if (value == _hasChanged) return;
				_hasChanged = value;
				NotifyOfPropertyChange();
			}
		}

		public bool RequiresChange
		{
			get { return _requiresChange; }
			set
			{
				if (value == _requiresChange) return;
				_requiresChange = value;
				NotifyOfPropertyChange();
			}
		}

		public bool HasError
		{
			get { return _hasError; }
			set
			{
				if (value == _hasError) return;
				_hasError = value;
				NotifyOfPropertyChange();
			}
		}

		public IEnumerable<string> ErrorMessages
		{
			get { return _errorMessages; }
			protected set
			{
				if (Equals(value, _errorMessages)) return;
				_errorMessages = value;
				NotifyOfPropertyChange();
			}
		}

		public string Label
		{
			get { return _label; }
			set
			{
				if (value == _label) return;
				_label = value;
				NotifyOfPropertyChange();
			}
		}

		public abstract void Validate();
	}

	public abstract class Field<T> : Field
	{
		private readonly T _initialValue;
		private T _value;

		public T Value
		{
			get { return _value; }
			set
			{
				if (Equals(value, _value)) return;
				_value = value;
				Validate();
				NotifyOfPropertyChange();
			}
		}

		public IList<IFieldValidationRule<T>> ValidationRules { get; }

		public Field()
			: this(default(T)) { }
		public Field(T initialValue)
		{
			_value = _initialValue = initialValue;
			ValidationRules = new List<IFieldValidationRule<T>>();
		}

		public override void Validate()
		{
			if (!RequiresChange && AreEqual(Value, _initialValue))
			{
				HasChanged = false;
				ErrorMessages = null;
				HasError = false;
			}
			else
			{
				ErrorMessages = ValidationRules.Select(r => r.Validate(Value))
				                               .Where(s => s != null)
				                               .ToList();
				HasError = ErrorMessages.Any();
				HasChanged = true;
			}
		}

		protected virtual bool AreEqual(T value, T initialValue)
		{
			return Equals(value, initialValue);
		}
	}
}