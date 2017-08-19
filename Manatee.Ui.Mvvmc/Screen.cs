using System;
using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// A base implementation of <see cref = "IScreen" />.
	/// </summary>
	public abstract class Screen : InitializableViewModel, IScreen, IChild
	{
		private static readonly ILog _log = LogManager.GetLog(typeof(Screen));

		private bool _isActive;
		private object _parent;
		private bool _hasBeenActivated;

		/// <summary>
		/// Gets or Sets the Parent <see cref = "IConductor" />
		/// </summary>
		public object Parent
		{
			get { return _parent; }
			set
			{
				_parent = value;
				NotifyOfPropertyChange();
			}
		}

		/// <summary>
		/// Indicates whether or not this instance is currently active.
		/// Virtualized in order to help with document oriented view models.
		/// </summary>
		public bool IsActive
		{
			get { return _isActive; }
			private set
			{
				_isActive = value;
				NotifyOfPropertyChange();
			}
		}

		/// <summary>
		/// Raised after activation occurs.
		/// </summary>
		public event EventHandler<ActivationEventArgs> Activated = delegate { };

		/// <summary>
		/// Raised before deactivation.
		/// </summary>
		public event EventHandler<AttemptingDeactivationEventArgs> AttemptingDeactivation = delegate { };

		/// <summary>
		/// Raised after deactivation.
		/// </summary>
		public event EventHandler<DeactivationEventArgs> Deactivated = delegate { };

		/// <summary>
		/// Raised after initialization, just before the first activation.
		/// </summary>
		public event EventHandler FirstActivation = delegate { };

		async Task IActivate.Activate()
		{
			if (IsActive) return;
			var firstActivation = false;
			if (!_hasBeenActivated)
			{
				_hasBeenActivated = firstActivation = true;
				await OnFirstActivation();
			}

			IsActive = true;
			await OnActivate(firstActivation);
		}

		async Task IDeactivate.Deactivate(bool close)
		{
			if (IsActive || (IsInitialized && close))
			{
				IsActive = false;
				await OnDeactivate(close);
			}
		}

		/// <summary>
		/// Called to check whether or not this instance can close.
		/// </summary>
		public virtual Task<bool> CanClose()
		{
			return Task.FromResult(true);
		}

		/// <summary>
		/// Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding view to close.
		/// Also provides an opportunity to pass a dialog result to it's corresponding view.
		/// </summary>
		public virtual async Task TryClose()
		{
			var conductor = Parent as IConductor;
			if (conductor != null)
			{
				await conductor.DeactivateItem(this, true);
				return;
			}

			var deactivate = (IDeactivate) this;
			await deactivate.Deactivate(true);
		}

		/// <summary>
		/// Called when initializing.
		/// </summary>
		protected virtual Task OnFirstActivation()
		{
			return Task.Run(() =>
				{
					_log.Info("Activating first time {0}.", this);
					RaiseEvent(FirstActivation);
				});
		}

		/// <summary>
		/// Called when activating.
		/// </summary>
		protected virtual Task OnActivate(bool firstActivation)
		{
			return Task.Run(() =>
				{
					_log.Info("Activating {0}.", this);
					RaiseEvent(Activated, new ActivationEventArgs(firstActivation));
				});
		}

		/// <summary>
		/// Called when attempting deactivating.
		/// </summary>
		/// <param name = "close">Inidicates whether this instance will be closed.</param>
		/// <returns>true if deactivation may proceed; false otherwise.</returns>
		protected virtual async Task<bool> OnAttemptingDeactivation(bool close)
		{
			_log.Info("Attempting Deactivation of {0}", this);
			var args = new AttemptingDeactivationEventArgs();
			RaiseEvent(AttemptingDeactivation, args);

			var shouldDeactivate = !args.Cancel && close && await CanClose();
			if (!shouldDeactivate)
				_log.Info("Could not deactivate {0}", this);

			return shouldDeactivate;
		}

		/// <summary>
		/// Called when deactivating.
		/// </summary>
		/// <param name = "close">Inidicates whether this instance will be closed.</param>
		protected virtual async Task OnDeactivate(bool close)
		{
			if (!await OnAttemptingDeactivation(close)) return;

			_log.Info("Deactivating {0}.", this);
			RaiseEvent(Deactivated, new DeactivationEventArgs(close));
			if (close)
				_log.Info("Closed {0}.", this);
		}
	}
}