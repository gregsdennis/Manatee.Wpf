namespace Manatee.Wpf.ViewModel
{
	/// <summary>
	/// <see cref="ViewModelBase"/> implementation that provides indication that a viewmodel
	/// requires initialization.  The default state is equivalent to "initialization complete."
	/// </summary>
	public abstract class InitializableViewModel : ViewModelBase, IInitialize
	{
		private bool _isInitialized;
		private bool _isInitializing;

		/// <summary>
		/// Indicates whether or not this instance is currently initialized.
		/// Virtualized in order to help with document oriented view models.
		/// </summary>
		public bool IsInitialized
		{
			get { return _isInitialized; }
			private set
			{
				_isInitialized = value;
				NotifyOfPropertyChange();
			}
		}

		/// <summary>
		/// Gets whether the view model is in the process of being initialized.
		/// </summary>
		public bool IsInitializing
		{
			get { return _isInitializing; }
			private set
			{
				if (value == _isInitializing) return;
				_isInitializing = value;
				NotifyOfPropertyChange();
			}
		}

		protected InitializableViewModel()
		{
			_isInitialized = true;
		}

		/// <summary>
		/// Sets properties to indicate initialization has begun.
		/// </summary>
		public void BeginInitialization()
		{
			IsInitialized = false;
			IsInitializing = true;
		}

		/// <summary>
		/// Sets properties to indicate initialization is complete.
		/// </summary>
		public void CompleteInitialization()
		{
			IsInitialized = true;
			IsInitializing = false;
		}
	}
}