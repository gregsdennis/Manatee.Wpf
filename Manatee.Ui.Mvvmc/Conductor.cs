using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// An implementation of <see cref="IConductor"/> that holds on to and activates only one item at a time.
	/// </summary>
	public abstract partial class Conductor<T> : ConductorBaseWithActiveItem<T>
		where T : class
	{
		/// <summary>
		/// Activates the specified item.
		/// </summary>
		/// <param name="item">The item to activate.</param>
		public override async Task ActivateItem(T item)
		{
			if (item != null && item.Equals(ActiveItem))
			{
				if (IsActive)
				{
					await item.TryActivate();
					OnActivationProcessed(item, true);
				}
				return;
			}

			if (ActiveItem is IGuardClose closeable && !await closeable.CanClose())
			{
				OnActivationProcessed(item, false);
				return;
			}
			await ChangeActiveItem(item, true);
		}

		/// <summary>
		/// Deactivates the specified item.
		/// </summary>
		/// <param name="item">The item to close.</param>
		/// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
		public override async Task DeactivateItem(T item, bool close)
		{
			if (item == null || !item.Equals(ActiveItem)) return;
			await item.TryDeactivate(close);
		}

		/// <summary>
		/// Called to check whether or not this instance can close.
		/// </summary>
		public override async Task<bool> CanClose()
		{
			var closable = ActiveItem as IGuardClose;
			return closable == null || await closable.CanClose();
		}

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <returns>The collection of children.</returns>
		public override IEnumerable<T> GetChildren()
		{
			return new[] {ActiveItem};
		}

		/// <summary>
		/// Called when activating.
		/// </summary>
		protected override async Task OnActivate(bool initialized)
		{
			await base.OnActivate(initialized);
			await ActiveItem.TryActivate();
		}

		/// <summary>
		/// Called when deactivating.
		/// </summary>
		/// <param name="close">Inidicates whether this instance will be closed.</param>
		protected override async Task OnDeactivate(bool close)
		{
			await ActiveItem.TryDeactivate(close);
			await base.OnDeactivate(close);
		}
	}
}