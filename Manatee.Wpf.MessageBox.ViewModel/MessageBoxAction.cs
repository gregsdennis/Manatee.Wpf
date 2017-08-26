namespace Manatee.Wpf.MessageBox.ViewModel
{
	/// <summary>
	/// Enumerates various message box result types.
	/// </summary>
	public enum MessageBoxAction
	{
		/// <summary>
		/// Indicates the user chose to cancel.
		/// </summary>
		Cancel,
		/// <summary>
		/// Indicates the user confirmed.
		/// </summary>
		Confirm,
		/// <summary>
		/// Indicates the user declined.
		/// </summary>
		Decline
	}
}