using System;

namespace Manatee.Ui.Mvvmc
{

	/// <summary>
	/// EventArgs sent during activation.
	/// </summary>
	public class ActivationEventArgs : EventArgs
	{
		/// <summary>
		/// Indicates whether the sender was initialized in addition to being activated.
		/// </summary>
		public bool IsFirstActivation { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="ActivationEventArgs"/>.
		/// </summary>
		/// <param name="isFirstActivation">Indicates whether the sender was initialized in addition to being activated.</param>
		public ActivationEventArgs(bool isFirstActivation)
		{
			IsFirstActivation = isFirstActivation;
		}
	}
}
