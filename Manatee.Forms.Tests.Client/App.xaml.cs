using System;
using System.Collections.Generic;
using System.Windows;
using Manatee.Wpf.Converters;

namespace Manatee.Wpf.Tests.Client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			EnumToUiString.RegisterType(new Dictionary<DayOfWeek,string>
				{
					[DayOfWeek.Sunday] = "The first day",
					[DayOfWeek.Monday] = "The worst day",
					[DayOfWeek.Tuesday] = "The forgotten day",
					[DayOfWeek.Wednesday] = "The hump day",
					[DayOfWeek.Thursday] = "Thor's day",
					[DayOfWeek.Friday] = "The fun day",
					[DayOfWeek.Saturday] = "The last day",
				});

			base.OnStartup(e);
		}
	}
}
