using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manatee.Wpf.Converters;
using NUnit.Framework;

namespace Manatee.Wpf.Tests
{
	public class BooleanInversionConverterTests
	{
		[TestCase(false, true)]
		[TestCase(true, false)]
		public void ConvertInverts(bool expected, object value)
		{
			var actual = BooleanInversionConverter.Instance.Convert(value, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ThrowsOnIncorrectType()
		{
			Assert.Throws<ArgumentException>(() => BooleanInversionConverter.Instance.Convert(1, typeof(bool), null, CultureInfo.CurrentCulture));
		}
	}
}
