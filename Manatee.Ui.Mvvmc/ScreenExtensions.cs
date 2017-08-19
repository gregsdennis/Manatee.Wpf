using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Hosts extension methods for <see cref="IScreen"/> classes.
	/// </summary>
	internal static class ScreenExtensions
	{
		/// <summary>
		/// Activates the item if it implements <see cref="IActivate"/>, otherwise does nothing.
		/// </summary>
		/// <param name="potentialActivatable">The potential activatable.</param>
		public static Task TryActivate(this object potentialActivatable)
		{
			var activator = potentialActivatable as IActivate;
			return activator?.Activate();
		}

		/// <summary>
		/// Deactivates the item if it implements <see cref="IDeactivate"/>, otherwise does nothing.
		/// </summary>
		/// <param name="potentialDeactivatable">The potential deactivatable.</param>
		/// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
		public static Task TryDeactivate(this object potentialDeactivatable, bool close)
		{
			var deactivator = potentialDeactivatable as IDeactivate;
			return deactivator?.Deactivate(close);
		}
	}
}
