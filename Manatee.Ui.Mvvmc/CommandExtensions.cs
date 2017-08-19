using System.Windows.Input;

namespace Manatee.Ui.Mvvmc
{
	public static class CommandExtensions
	{
		public static void Refresh(this ICommand command)
		{
			PlatformProvider.Current.InvalidateRequerySuggested();
		}
	}
}
