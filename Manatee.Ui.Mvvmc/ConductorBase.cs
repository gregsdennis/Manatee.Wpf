﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// A base class for various implementations of <see cref="IConductor"/>.
	/// </summary>
	/// <typeparam name="T">The type that is being conducted.</typeparam>
	public abstract class ConductorBase<T> : Screen, IConductor, IParent<T> where T : class
	{
		void IConductor.ActivateItem(object item)
		{
			ActivateItem((T) item);
		}

		Task IConductor.DeactivateItem(object item, bool close)
		{
			return DeactivateItem((T) item, close);
		}

		IEnumerable IParent.GetChildren()
		{
			return GetChildren();
		}

		/// <summary>
		/// Occurs when an activation request is processed.
		/// </summary>
		public virtual event EventHandler<ActivationProcessedEventArgs> ActivationProcessed = delegate { };

		/// <summary>
		/// Gets the children.
		/// </summary>
		/// <returns>The collection of children.</returns>
		public abstract IEnumerable<T> GetChildren();

		/// <summary>
		/// Activates the specified item.
		/// </summary>
		/// <param name="item">The item to activate.</param>
		public abstract Task ActivateItem(T item);

		/// <summary>
		/// Deactivates the specified item.
		/// </summary>
		/// <param name="item">The item to close.</param>
		/// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
		public abstract Task DeactivateItem(T item, bool close);

		/// <summary>
		/// Called by a subclass when an activation needs processing.
		/// </summary>
		/// <param name="item">The item on which activation was attempted.</param>
		/// <param name="success">if set to <c>true</c> activation was successful.</param>
		protected virtual void OnActivationProcessed(T item, bool success)
		{
			if (item == null) return;

			ActivationProcessed?.Invoke(this, new ActivationProcessedEventArgs(item, success));
		}

		/// <summary>
		/// Ensures that an item is ready to be activated.
		/// </summary>
		/// <param name="newItem">The item that is about to be activated.</param>
		/// <returns>The item to be activated.</returns>
		protected virtual T EnsureItem(T newItem)
		{
			var node = newItem as IChild;
			if (node != null && node.Parent != this)
				node.Parent = this;

			return newItem;
		}
	}
}