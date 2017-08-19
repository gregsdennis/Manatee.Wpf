using System;
using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Denotes an instance which requires activation.
	/// </summary>
	public interface IActivate
	{
		///<summary>
		/// Indicates whether or not this instance is active.
		///</summary>
		bool IsActive { get; }

		/// <summary>
		/// Raised after activation occurs.
		/// </summary>
		event EventHandler<ActivationEventArgs> Activated;

		/// <summary>
		/// Activates this instance.
		/// </summary>
		Task Activate();
	}
}
