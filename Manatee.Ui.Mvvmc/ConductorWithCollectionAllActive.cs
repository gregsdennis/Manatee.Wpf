using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
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
			/// An implementation of <see cref="IConductor"/> that holds on to many items wich are all activated.
			/// </summary>
			public abstract class AllActive : ConductorBase<T>
			{
				private readonly BindableCollection<T> _items = new BindableCollection<T>();
				private readonly bool _openPublicItems;

				/// <summary>
				/// Gets the items that are currently being conducted.
				/// </summary>
				public IObservableCollection<T> Items => _items;

				/// <summary>
				/// Initializes a new instance of the <see cref="Conductor&lt;T&gt;.Collection.AllActive"/> class.
				/// </summary>
				/// <param name="openPublicItems">if set to <c>true</c> opens public items that are properties of this class.</param>
				protected AllActive(bool openPublicItems)
					: this()
				{
					_openPublicItems = openPublicItems;
				}

				/// <summary>
				/// Initializes a new instance of the <see cref="Conductor&lt;T&gt;.Collection.AllActive"/> class.
				/// </summary>
				public AllActive()
				{
					_items.CollectionChanged += (s, e) =>
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
									_items.OfType<IChild>().Apply(x => x.Parent = this);
									break;
							}
						};
				}

				/// <summary>
				/// Called to check whether or not this instance can close.
				/// </summary>
				public override async Task<bool> CanClose()
				{
					var guardClosable = Items.OfType<IGuardClose>();
					var itemsCanClose = (await Task.WhenAll(guardClosable.Select(gc => gc.CanClose()))).Aggregate(true, (current, result) => current && result);

					return itemsCanClose;
				}

				/// <summary>
				/// Activates the specified item.
				/// </summary>
				/// <param name="item">The item to activate.</param>
				public override async Task ActivateItem(T item)
				{
					if (item == null) return;

					item = EnsureItem(item);

					if (IsActive)
						await item.TryActivate();

					OnActivationProcessed(item, true);
				}

				/// <summary>
				/// Deactivates the specified item.
				/// </summary>
				/// <param name="item">The item to close.</param>
				/// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
				public override async Task DeactivateItem(T item, bool close)
				{
					if (item == null) return;

					if (close)
					{
						if (item is IGuardClose closeable && !await closeable.CanClose()) return;

						await _CloseItemCore(item);
					}
					else
						await item.TryDeactivate(false);
				}

				/// <summary>
				/// Gets the children.
				/// </summary>
				/// <returns>The collection of children.</returns>
				public override IEnumerable<T> GetChildren()
				{
					return _items;
				}

				/// <summary>
				/// Called when activating.
				/// </summary>
				protected override Task OnActivate(bool initialized)
				{
					return Task.WhenAll(_items.OfType<IActivate>().Select(x => x.Activate()));
				}

				/// <summary>
				/// Called when deactivating.
				/// </summary>
				/// <param name="close">Inidicates whether this instance will be closed.</param>
				protected override async Task OnDeactivate(bool close)
				{
					await Task.WhenAll(_items.OfType<IDeactivate>().Select(x => x.Deactivate(close)));
					if (close)
						_items.Clear();
					if (!IsActive)
						await base.OnDeactivate(close);
				}

				/// <summary>
				/// Called when initializing.
				/// </summary>
				protected override async Task OnFirstActivation()
				{
					if (_openPublicItems)
					{
						var typeinfo = typeof(T).GetTypeInfo();
						await Task.WhenAll(GetType().GetTypeInfo().DeclaredProperties
						         .Where(x => x.Name != "Parent" && typeinfo.IsAssignableFrom(x.PropertyType.GetTypeInfo()))
						         .Select(x => x.GetValue(this, null))
						         .Cast<T>()
						         .Select(ActivateItem));
					}
				}

				/// <summary>
				/// Ensures that an item is ready to be activated.
				/// </summary>
				/// <param name="newItem">The item that is about to be activated.</param>
				/// <returns>The item to be activated.</returns>
				protected override T EnsureItem(T newItem)
				{
					var index = _items.IndexOf(newItem);

					if (index == -1)
						_items.Add(newItem);
					else
						newItem = _items[index];

					return base.EnsureItem(newItem);
				}

				private async Task _CloseItemCore(T item)
				{
					await item.TryDeactivate(true);
					_items.Remove(item);
				}
			}
		}
	}
}
