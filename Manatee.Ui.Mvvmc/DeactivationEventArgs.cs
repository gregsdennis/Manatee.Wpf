using System;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// EventArgs sent during deactivation.
	/// </summary>
	public class DeactivationEventArgs : EventArgs
	{
		/// <summary>
		/// Indicates whether the sender was closed in addition to being deactivated.
		/// </summary>
		public bool WasClosed { get; }

		/// <summary>
		/// Initializes a new instance of <see cref="DeactivationEventArgs"/>.
		/// </summary>
		/// <param name="wasClosed">Indicates whether the sender was closed in addition to being deactivated.</param>
		public DeactivationEventArgs(bool wasClosed)
		{
			WasClosed = wasClosed;
		}
	}
}
