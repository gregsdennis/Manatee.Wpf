using System;
using System.Globalization;
using System.Windows;
using Manatee.Wpf.Converters;
using NUnit.Framework;

namespace Manatee.Wpf.Tests.Converters
{
	public class BooleanLogicTests
	{
		[TestCase(true, new object[] {true, true, true})]
		[TestCase(false, new object[] {true, false, true})]
		[TestCase(false, new object[] {false, false, false})]
		[TestCase(false, new object[] {false, null, false})]
		public void And(bool expected, object[] input)
		{
			var actual = BooleanLogic.And.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[TestCase(false, new object[] {true, true, true})]
		[TestCase(true, new object[] {true, false, true})]
		[TestCase(true, new object[] {false, false, false})]
		public void Nand(bool expected, object[] input)
		{
			var actual = BooleanLogic.Nand.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[TestCase(true, new object[] {true, true, true})]
		[TestCase(true, new object[] {true, false, true})]
		[TestCase(false, new object[] {false, false, false})]
		public void Or(bool expected, object[] input)
		{
			var actual = BooleanLogic.Or.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[TestCase(false, new object[] {true, true, true})]
		[TestCase(false, new object[] {true, false, true})]
		[TestCase(true, new object[] {false, false, false})]
		public void Nor(bool expected, object[] input)
		{
			var actual = BooleanLogic.Nor.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[TestCase(true, new object[] {true, true, true})]
		[TestCase(false, new object[] {true, false, true})]
		[TestCase(true, new object[] {true, false, false})]
		[TestCase(false, new object[] {false, false, false})]
		public void XorOdd(bool expected, object[] input)
		{
			var actual = BooleanLogic.XorOdd.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[TestCase(false, new object[] {true, true, true})]
		[TestCase(true, new object[] {true, false, true})]
		[TestCase(false, new object[] {true, false, false})]
		[TestCase(true, new object[] {false, false, false})]
		public void XnorOdd(bool expected, object[] input)
		{
			var actual = BooleanLogic.XnorOdd.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[TestCase(false, new object[] {true, true, true})]
		[TestCase(false, new object[] {true, false, true})]
		[TestCase(true, new object[] {true, false, false})]
		[TestCase(false, new object[] {false, false, false})]
		public void XorSingle(bool expected, object[] input)
		{
			var actual = BooleanLogic.XorSingle.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[TestCase(true, new object[] {true, true, true})]
		[TestCase(true, new object[] {true, false, true})]
		[TestCase(false, new object[] {true, false, false})]
		[TestCase(true, new object[] {false, false, false})]
		public void XnorSingle(bool expected, object[] input)
		{
			var actual = BooleanLogic.XnorSingle.Convert(input, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ThrowsOnIncorrectType()
		{
			Assert.Throws<ArgumentException>(() => BooleanLogic.And.Convert(new object[] {true, 1}, typeof(bool), null, CultureInfo.CurrentCulture));
		}

		[Test]
		public void ThrowsOnNoItems()
		{
			Assert.Throws<ArgumentException>(() => BooleanLogic.And.Convert(new object[] {}, typeof(bool), null, CultureInfo.CurrentCulture));
		}

		[Test]
		public void IgnoresDependencyPropertyUnsetValue()
		{
			var values = new[] { false, DependencyProperty.UnsetValue, true };
			var actual = BooleanLogic.And.Convert(values, typeof(bool), null, CultureInfo.CurrentCulture);

			Assert.AreSame(values, actual);
		}
	}
}