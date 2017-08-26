using System.Windows;
using System.Windows.Controls;

namespace Manatee.Wpf
{
	/// <summary>
	/// Interaction logic for BusySpinner.xaml
	/// </summary>
	public partial class BusySpinner : UserControl, IAutomate
	{
		public DependencyProperty AutomationProperty => VisibilityProperty;

		public BusySpinner()
		{
			InitializeComponent();
		}
	}
}
