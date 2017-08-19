using System;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// EventArgs sent before deactivation.
	/// </summary>
	public class AttemptingDeactivationEventArgs : EventArgs
	{
		/// <summary>
		/// Gets or sets whether the close action should be cancelled.
		/// </summary>
		public bool Cancel { get; set; }
	}
}