using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	public abstract partial class Conductor<T>
	{
		/// <summary>
		/// An implementation of <see cref="IConductor"/> that holds on many items.
		/// </summary>
		public abstract partial class Collection
		{
			/// <summary>
			/// An implementation of <see cref="IConductor"/> that holds on many items but only activates one at a time.
			/// </summary>
			public abstract class OneActive : ConductorBaseWithActiveItem<T>
			{
				/// <summary>
				/// Initializes a new instance of the <see cref="Conductor&lt;T&gt;.Collection.OneActive"/> class.
				/// </summary>
				protected OneActive()
				{
					Items.CollectionChanged += (s, e) =>
						{
							switch (e.Action)
							{
								case NotifyCollectionChangedAction.Add:
									e.NewItems.OfType<IChild>().Apply(x => x.Parent = this);
									break;
								case NotifyCollectionChangedAction.Remove:
									e.OldItems.OfType<IChild>().Apply(x => x.Parent = null);
									break;
								case NotifyCollectionChangedAction.Replace:
									e.NewItems.OfType<IChild>().Apply(x => x.Parent = this);
									e.OldItems.OfType<IChild>().Apply(x => x.Parent = null);
									break;
								case NotifyCollectionChangedAction.Reset:
									Items.OfType<IChild>().Apply(x => x.Parent = this);
									break;
							}
						};
				}

				/// <summary>
				/// Gets the items that are currently being conducted.
				/// </summary>
				public IObservableCollection<T> Items { get; } = new BindableCollection<T>();

				/// <summary>
				/// Gets the children.
				/// </summary>
				/// <returns>The collection of children.</returns>
				public override IEnumerable<T> GetChildren()
				{
					return Items;
				}

				/// <summary>
				/// Activates the specified item.
				/// </summary>
				/// <param name="item">The item to activate.</param>
				public override async Task ActivateItem(T item)
				{
					if (ActiveItem is IGuardClose guardClose)
					{
						var canCloseActiveItem = await guardClose.CanClose();
						if (!canCloseActiveItem) return;
					}

					if (item != null && item.Equals(ActiveItem))
					{
						if (!IsActive) return;

						await item.TryActivate();
						OnActivationProcessed(item, true);

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
					if (item == null) return;

					if (!close)
						await item.TryDeactivate(false);
					else
					{
						if (item is IGuardClose closable && await closable.CanClose())
							await _CloseItemCore(item);
					}
				}

				/// <summary>
				/// Called to check whether or not this instance can close.
				/// </summary>
				public override async Task<bool> CanClose()
				{
					return ActiveItem is IGuardClose guardClose && await guardClose.CanClose();
				}

				/// <summary>
				/// Called when activating.
				/// </summary>
				protected override Task OnActivate(bool initialized)
				{
					return ActiveItem.TryActivate();
				}

				/// <summary>
				/// Determines the next item to activate based on the last active index.
				/// </summary>
				/// <param name="list">The list of possible active items.</param>
				/// <param name="lastIndex">The index of the last active item.</param>
				/// <returns>The next item to activate.</returns>
				/// <remarks>Called after an active item is closed.</remarks>
				protected virtual T DetermineNextItemToActivate(IList<T> list, int lastIndex)
				{
					var toRemoveAt = lastIndex - 1;

					if (toRemoveAt == -1 && list.Count > 1) return list[1];

					if (toRemoveAt > -1 && toRemoveAt < list.Count - 1) return list[toRemoveAt];

					return default(T);
				}

				/// <summary>
				/// Called when deactivating.
				/// </summary>
				/// <param name="close">Inidicates whether this instance will be closed.</param>
				protected override async Task OnDeactivate(bool close)
				{
					if (close)
					{
						await Task.WhenAll(Items.OfType<IDeactivate>().Select(x => x.Deactivate(true)));
						Items.Clear();
					}
					else
						await ActiveItem.TryDeactivate(false);
					await base.OnDeactivate(close);
				}

				/// <summary>
				/// Ensures that an item is ready to be activated.
				/// </summary>
				/// <param name="newItem">The item that is about to be activated.</param>
				/// <returns>The item to be activated.</returns>
				protected override T EnsureItem(T newItem)
				{
					if (newItem == null)
						newItem = DetermineNextItemToActivate(Items, ActiveItem != null ? Items.IndexOf(ActiveItem) : 0);
					else
					{
						var index = Items.IndexOf(newItem);

						if (index == -1)
							Items.Add(newItem);
						else newItem = Items[index];
					}

					return base.EnsureItem(newItem);
				}

				private async Task _CloseItemCore(T item)
				{
					if (item.Equals(ActiveItem))
					{
						var index = Items.IndexOf(item);
						var next = DetermineNextItemToActivate(Items, index);

						await ChangeActiveItem(next, true);
					}
					else
						await item.TryDeactivate(true);

					Items.Remove(item);
				}
			}
		}
	}
}
