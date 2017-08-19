using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Interface for platform specific operations that need enlightenment.
	/// </summary>
	public interface IPlatformProvider
	{
		#region Execute

		/// <summary>
		///   Indicates whether or not the framework is in design-time mode.
		/// </summary>
		bool IsInDesignMode { get; }

		/// <summary>
		///   Executes the action on the UI thread asynchronously.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		Task BeginOnUiThread([NotNull] Action action);

		/// <summary>
		///   Executes the action on the UI thread.
		/// </summary>
		/// <param name = "action">The action to execute.</param>
		void OnUiThread([NotNull] Action action);

		#endregion

		#region Command

		/// <summary>
		/// Occurs when the system detects conditions that might change the ability of a command to execute.
		/// </summary>
		event EventHandler RequerySuggested;
		/// <summary>
		/// Forces the system to raise the <see cref="RequerySuggested"/> event.
		/// </summary>
		void InvalidateRequerySuggested();

		#endregion
	}
}
