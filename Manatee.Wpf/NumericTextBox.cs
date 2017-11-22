using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Manatee.Wpf
{
	/// <summary>
	///     A text entry box that requires numeric values.
	/// </summary>
	public class NumericTextBox : TextBox, IAutomate
	{
		private static readonly decimal _maxValue = (decimal) Math.Pow(10.0, 27.0);

		private readonly StringBuilder _numericRawValue = new StringBuilder();
		private int _digitsAfterCursor;
		private int _multiplier;

		/// <summary>
		///     Indicates the numeric value.
		/// </summary>
		public static readonly DependencyProperty NumericValueProperty;
		/// <summary>
		///     Indicates the number format.
		/// </summary>
		public static readonly DependencyProperty FormatProperty;

		/// <summary>
		///     Indicates the numeric value.
		/// </summary>
		public decimal? NumericValue
		{
			get { return (decimal?) GetValue(NumericValueProperty); }
			set { SetValue(NumericValueProperty, value); }
		}
		/// <summary>
		///     Indicates the number format.
		/// </summary>
		public string Format
		{
			get { return (string) GetValue(FormatProperty); }
			set { SetValue(FormatProperty, value); }
		}
		public DependencyProperty AutomationProperty => NumericValueProperty;

		static NumericTextBox()
		{
			NumericValueProperty = DependencyProperty.Register("NumericValue", typeof(decimal?), typeof(NumericTextBox),
			                                                   new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, _OnNumericValueChanged, _CoerceNumericValue)
				                                                   {
					                                                   BindsTwoWayByDefault = true,
					                                                   DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
				                                                   });
			FormatProperty = DependencyProperty.Register("Format", typeof(string), typeof(NumericTextBox), new PropertyMetadata("G", _OnFormatChanged));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericTextBox), new FrameworkPropertyMetadata(typeof(NumericTextBox)));
		}

		/// <summary>
		///     Creates a new instance of <see cref="T:StoreSystems.Ui.Controls.NumericTextBox" />.
		/// </summary>
		public NumericTextBox()
		{
			Loaded += _NumericTextBoxLoaded;
			Unloaded += _NumericTextBoxUnloaded;
			_multiplier = 1;
		}

		private void _NumericTextBoxUnloaded(object sender, RoutedEventArgs e)
		{
			DataObject.RemovePastingHandler(this, _Pasting);
		}

		private void _NumericTextBoxLoaded(object sender, RoutedEventArgs e)
		{
			DataObject.AddPastingHandler(this, _Pasting);
			_SetTextToNumericValue();
		}

		private void _SetTextToNumericValue()
		{
			if (!NumericValue.HasValue)
			{
				Text = null;
				return;
			}
			var text = NumericValue.Value.ToString(Format);
			if (Text == text) return;
			Text = text;
			_numericRawValue.Clear();
			_numericRawValue.Append(new string(Text.Where(char.IsDigit).ToArray()));
			var num = 0;
			var num2 = 0;
			var num3 = Text.Count(char.IsDigit);
			var num4 = num3 - _digitsAfterCursor;
			while (num < Text.Length && num2 < num4)
			{
				if (char.IsDigit(Text[num]))
					num2++;
				num++;
			}
			SelectionLength = 0;
			SelectionStart = num;
		}

		private static void _OnNumericValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
		{
			var numericTextBox = sender as NumericTextBox;
			numericTextBox?._SetTextToNumericValue();
		}

		/// <summary>
		///     Handles paste operation if in numeric data restriction mode by disallowing the
		///     paste if it does not fit numeric restrictions.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void _Pasting(object sender, DataObjectPastingEventArgs e)
		{
			if (!(sender is NumericTextBox numericTextBox)) return;
			if (e.DataObject.GetDataPresent(typeof(string)))
			{
				var source = (string) e.DataObject.GetData(typeof(string));
				var text = new string(source.Where(char.IsDigit).ToArray());
				if (string.IsNullOrEmpty(text)) return;
				numericTextBox._InsertPastedDigits(text);
				numericTextBox._UpdateNumericValue();
			}
			e.Handled = true;
			e.CancelCommand();
		}

		/// <summary>
		///     Handles key entry in event of numeric data entry restriction.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			if (!e.Handled)
			{
				if (e.Key == Key.Back || e.Key == Key.Delete)
				{
					_HandleBackKey(e.Key);
					e.Handled = true;
				}
				else
				{
					e.Handled = _CheckMaxLengthAndHandleInputKey(e);
				}
				_UpdateNumericValue();
				if (e.Key == Key.Space)
					e.Handled = true;
			}
			base.OnPreviewKeyDown(e);
		}

		private void _HandleBackKey(Key key)
		{
			if (_numericRawValue.Length <= 0 || NumericValue == 0)
			{
				_numericRawValue.Clear();
				NumericValue = null;
				return;
			}
			_numericRawValue.Clear();
			_numericRawValue.Append(new string(Text.Where(char.IsDigit).ToArray()));
			var num = Text.Substring(0, SelectionStart + SelectionLength).Where(char.IsDigit).Count();
			_digitsAfterCursor = _numericRawValue.Length - num;
			var startIndex = Text.Substring(0, SelectionStart).Where(char.IsDigit).Count();
			var num2 = SelectionLength == 0 ? 0 : Text.Substring(SelectionStart, SelectionLength).Count(char.IsDigit);
			if (num2 != 0)
			{
				_numericRawValue.Remove(startIndex, num2);
				return;
			}
			if (key == Key.Back && num > 0)
			{
				_numericRawValue.Remove(num - 1, 1);
				return;
			}
			if (key == Key.Delete && _digitsAfterCursor != 0)
			{
				_numericRawValue.Remove(num, 1);
				_digitsAfterCursor--;
			}
		}

		private void _InsertPastedDigits(string digits)
		{
			_numericRawValue.Clear();
			_numericRawValue.Append(new string(Text.Where(char.IsDigit).ToArray()));
			var num = Text.Substring(0, SelectionStart + SelectionLength).Where(char.IsDigit).Count();
			_digitsAfterCursor = _numericRawValue.Length - num;
			var num2 = Text.Substring(0, SelectionStart).Where(char.IsDigit).Count();
			var num3 = SelectionLength == 0 ? 0 : Text.Substring(SelectionStart, SelectionLength).Count(char.IsDigit);
			if (num3 != 0)
				_numericRawValue.Remove(num2, num3);
			var num4 = Math.Min(MaxLength, 28) - _numericRawValue.Length;
			if (num4 <= 0)
				return;
			if (digits.Length > num4)
				digits = digits.Substring(0, num4);
			_numericRawValue.Insert(Math.Min(num2, _numericRawValue.Length), digits);
		}

		private bool _CheckMaxLengthAndHandleInputKey(KeyEventArgs e)
		{
			if (MaxLength == 0)
				return _HandleInputKey(e);
			return MaxLength != 0 && Text.Length <= MaxLength && _HandleInputKey(e);
		}

		private bool _HandleInputKey(KeyEventArgs e)
		{
			var charFromKey = e.Key.GetCharFromKey();
			return _HandleInputCharacter(charFromKey);
		}

		private bool _HandleInputCharacter(char inputChar)
		{
			if (char.IsDigit(inputChar))
			{
				_numericRawValue.Clear();
				_numericRawValue.Append(new string(Text.Where(char.IsDigit).ToArray()));
				var num = Text.Substring(0, SelectionStart + SelectionLength).Where(char.IsDigit).Count();
				_digitsAfterCursor = _numericRawValue.Length - num;
				var num2 = Text.Substring(0, SelectionStart).Where(char.IsDigit).Count();
				var num3 = SelectionLength == 0 ? 0 : Text.Substring(SelectionStart, SelectionLength).Count(char.IsDigit);
				if (num3 != 0)
					_numericRawValue.Remove(num2, num3);
				_numericRawValue.Insert(Math.Min(num2, _numericRawValue.Length), inputChar);
				return true;
			}
			return !char.IsControl(inputChar) && inputChar != ' ';
		}

		private void _UpdateNumericValue()
		{
			NumericValue = _numericRawValue.Length > 0 ? decimal.Parse(_numericRawValue.ToString()) / _multiplier : (decimal?) null;
		}

		private static object _CoerceNumericValue(DependencyObject d, object value)
		{
			if (!(d is NumericTextBox numericTextBox)) return value;

			if (value == null || (decimal?) value <= _maxValue / numericTextBox._multiplier) return value;
			return numericTextBox.NumericValue;
		}

		private static void _OnFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!(d is NumericTextBox numericTextBox)) return;
			if (!(e.NewValue is string text))
				throw new ArgumentNullException(nameof(Format));

			var text2 = 0.ToString(text);
			var num = text2.LastIndexOf('.');
			var num2 = text2.LastIndexOfAny(Enumerable.Range(48, 57).Select(Convert.ToChar).ToArray());
			numericTextBox._multiplier = Math.Max(num == -1 ? 1 : (int) Math.Pow(10.0, num2 - num), 1);
			if (numericTextBox.Format.Contains(NumberFormatInfo.InvariantInfo.PercentSymbol) || numericTextBox.Format.ToLower() == "p")
				numericTextBox._multiplier *= 100;
			numericTextBox._SetTextToNumericValue();
		}
	}
}