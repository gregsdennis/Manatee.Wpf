using System.Windows;
using System.Windows.Controls;

namespace Manatee.Wpf
{
	/// <summary>
	/// Interaction logic for BusySpinner.xaml
	/// </summary>
	public partial class BusySpinner : UserControl, IAutomate
	{
		/// <summary>
		///     Gets the <see cref="T:System.Windows.DependencyProperty" /> which is used to extract the automation ID.
		/// </summary>
		public DependencyProperty AutomationProperty => VisibilityProperty;

		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		public BusySpinner()
		{
			InitializeComponent();
		}
	}
}
