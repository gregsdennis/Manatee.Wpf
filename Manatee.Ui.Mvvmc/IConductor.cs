using System;
using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Denotes an instance which conducts other objects by managing an ActiveItem and maintaining a strict lifecycle.
	/// </summary>
	/// <remarks>Conducted instances can opt into the lifecycle by impelenting any of the follosing <see cref="IActivate"/>, <see cref="IDeactivate"/>, <see cref="IGuardClose"/>.</remarks>
	public interface IConductor : IParent, INotifyPropertyChangedEx
	{
		/// <summary>
		/// Occurs when an activation request is processed.
		/// </summary>
		event EventHandler<ActivationProcessedEventArgs> ActivationProcessed;

		/// <summary>
		/// Activates the specified item.
		/// </summary>
		/// <param name="item">The item to activate.</param>
		void ActivateItem(object item);

		/// <summary>
		/// Deactivates the specified item.
		/// </summary>
		/// <param name="item">The item to close.</param>
		/// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
		Task DeactivateItem(object item, bool close);
	}
}
