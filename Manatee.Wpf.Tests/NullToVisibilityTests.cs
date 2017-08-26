using System.Windows;
using Manatee.Wpf.Converters;
using NUnit.Framework;

namespace Manatee.Wpf.Tests
{
	public class NullToVisibilityTests
	{
		[TestCase("test", Visibility.Visible)]
		[TestCase(null, Visibility.Hidden)]
		public void NullToHidden(object value, Visibility expected)
		{
			var actual = NullToVisibilityConverter.NullToHidden.Convert(value, null, null, null);

			Assert.AreEqual(expected, actual);
		}
	}
}
