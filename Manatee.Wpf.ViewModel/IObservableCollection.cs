using System.Collections.Generic;
using System.Collections.Specialized;
using JetBrains.Annotations;

namespace Manatee.Wpf.ViewModel
{
	/// <summary>
	/// Represents a collection that is observable.
	/// </summary>
	/// <typeparam name = "T">The type of elements contained in the collection.</typeparam>
	public interface IObservableCollection<T> : IList<T>, INotifyCollectionChanged
	{
		/// <summary>
		///   Adds the range.
		/// </summary>
		/// <param name = "items">The items.</param>
		void AddRange([NotNull] IEnumerable<T> items);

		/// <summary>
		///   Removes the range.
		/// </summary>
		/// <param name = "items">The items.</param>
		void RemoveRange([NotNull] IEnumerable<T> items);
	}
}
