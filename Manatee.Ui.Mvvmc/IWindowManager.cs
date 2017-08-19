namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// A service that manages windows.
	/// </summary>
	public interface IWindowManager
	{
		/// <summary>
		/// Shows a modal dialog for the specified model.
		/// </summary>
		/// <param name="rootModel">The root model.</param>
		/// <returns>The dialog result.</returns>
		void ShowDialog(IScreen rootModel);

		/// <summary>
		/// Shows a non-modal window for the specified model.
		/// </summary>
		/// <param name="rootModel">The root model.</param>
		void ShowWindow(IScreen rootModel);
	}
}