﻿using System;
using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Denotes an instance which requires deactivation.
	/// </summary>
	public interface IDeactivate
	{
		/// <summary>
		/// Raised before deactivation.
		/// </summary>
		event EventHandler<AttemptingDeactivationEventArgs> AttemptingDeactivation;

		/// <summary>
		/// Raised after deactivation.
		/// </summary>
		event EventHandler<DeactivationEventArgs> Deactivated;

		/// <summary>
		/// Deactivates this instance.
		/// </summary>
		/// <param name="close">Indicates whether or not this instance is being closed.</param>
		Task Deactivate(bool close);
	}
}
