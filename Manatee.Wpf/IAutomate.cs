using System.Windows;

namespace Manatee.Wpf
{
	/// <summary>
	///     Implementers will use AutomationProperty to indicate which property contains the binding from which to
	///     generate the automationID.
	/// </summary>
	public interface IAutomate
	{
		/// <summary>
		///     Gets the <see cref="T:System.Windows.DependencyProperty" /> which is used to extract the automation ID.
		/// </summary>
		DependencyProperty AutomationProperty { get; }
	}
}