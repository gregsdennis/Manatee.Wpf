using System;
using System.Globalization;
using Manatee.Wpf.Converters;
using NUnit.Framework;

namespace Manatee.Wpf.Tests.Converters
{
	public class BooleanInverterTests
	{
		[TestCase(false, true)]
		[TestCase(true, false)]
		public void ConvertInverts(bool expected, object value)
		{
			var actual = BooleanInverter.Instance.Convert(value, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ThrowsOnIncorrectType()
		{
			Assert.Throws<ArgumentException>(() => BooleanInverter.Instance.Convert(1, typeof(bool), null, CultureInfo.CurrentCulture));
		}
	}
}
