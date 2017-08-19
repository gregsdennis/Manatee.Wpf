using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// A base class for various implementations of <see cref="IConductor"/> that maintain an active item.
	/// </summary>
	/// <typeparam name="T">The type that is being conducted.</typeparam>
	public abstract class ConductorBaseWithActiveItem<T> : ConductorBase<T>, IConductActiveItem
		where T : class
	{
		private T _activeItem;

		/// <summary>
		/// The currently active item.
		/// </summary>
		public T ActiveItem
		{
			get { return _activeItem; }
			set { ActivateItem(value); }
		}

		/// <summary>
		/// The currently active item.
		/// </summary>
		/// <value></value>
		object IHaveActiveItem.ActiveItem
		{
			get { return ActiveItem; }
			set { ActiveItem = (T) value; }
		}

		/// <summary>
		/// Changes the active item.
		/// </summary>
		/// <param name="newItem">The new item to activate.</param>
		/// <param name="closePrevious">Indicates whether or not to close the previous active item.</param>
		protected virtual async Task ChangeActiveItem(T newItem, bool closePrevious)
		{
			await _activeItem.TryDeactivate(closePrevious);

			newItem = EnsureItem(newItem);

			if (IsActive)
				await newItem.TryActivate();

			_activeItem = newItem;
			NotifyOfPropertyChange(nameof(ActiveItem));
			OnActivationProcessed(_activeItem, true);
		}
	}
}