using System;

namespace Manatee.Ui.Mvvmc
{

	/// <summary>
	/// Contains details about the success or failure of an item's activation through an <see cref="IConductor"/>.
	/// </summary>
	public class ActivationProcessedEventArgs : EventArgs
	{
		/// <summary>
		/// The item whose activation was processed.
		/// </summary>
		public object Item { get; }

		/// <summary>
		/// Gets or sets a value indicating whether the activation was a success.
		/// </summary>
		/// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
		public bool Success { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="ActivationProcessedEventArgs"/>.
		/// </summary>
		/// <param name="item">The item whose activation was processed.</param>
		/// <param name="success">A value indicating whether the activation was a success.</param>
		public ActivationProcessedEventArgs(object item, bool success)
		{
			Item = item;
			Success = success;
		}
	}
}
