using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Manatee.Wpf
{
	public class MillerColumnView : ItemsControl
	{
		public static readonly DependencyProperty ColumnWidthProperty =
			DependencyProperty.Register("ColumnWidth", typeof(double), typeof(MillerColumnView), new PropertyMetadata(75.0));

		private readonly ObservableCollection<MillerColumnColumn> _columns;

		public IEnumerable Columns => _columns;

		public double ColumnWidth
		{
			get { return (double)GetValue(ColumnWidthProperty); }
			set { SetValue(ColumnWidthProperty, value); }
		}

		static MillerColumnView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MillerColumnView), new FrameworkPropertyMetadata(typeof(MillerColumnView)));
		}
		public MillerColumnView()
		{
			_columns = new ObservableCollection<MillerColumnColumn>();
		}

		protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
		{
			_RemoveFrom(0);

			_Add(newValue);
		}

		private void _Add(IEnumerable newValue)
		{
			if (newValue == null) return;

			var column = new MillerColumnColumn
				{
					ItemsSource = newValue,
					ItemTemplate = ItemTemplate
				};
			column.SelectionChanged += _SelectionChanged;

			_columns.Add(column);
		}

		private void _RemoveFrom(int startIndex)
		{
			foreach (var column in _columns.Skip(startIndex).Reverse().ToList())
			{
				column.SelectionChanged -= _SelectionChanged;

				_Remove(column);
			}
		}

		private void _SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(sender is MillerColumnColumn column)) return;

			var index = _columns.IndexOf(column);
			if (index == -1) return;

			var item = column.Items[index];
			_RemoveFrom(index+1);
			var container = column.ItemContainerGenerator.ContainerFromIndex(column.SelectedIndex) as ListBoxItem;
			var template = container?.ContentTemplate as HierarchicalDataTemplate;
			var sourceBinding = template?.ItemsSource as Binding;
			var clonedBinding = sourceBinding?.Clone(item);
			var children = clonedBinding?.Eval() as IEnumerable;

			_Add(children);
		}

		private void _Remove(MillerColumnColumn	 column)
		{
			_columns.Remove(column);
		}
	}

	internal static class BindingExtensions
	{
		private static readonly DependencyProperty DummyProperty = DependencyProperty.RegisterAttached(
			"Dummy", typeof(object), typeof(DependencyObject), new UIPropertyMetadata(null));

		public static Binding Clone(this Binding binding, object source)
		{
			var result = new Binding
				{
					Source = source,
					UpdateSourceTrigger = binding.UpdateSourceTrigger,
					NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
					NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
					NotifyOnValidationError = binding.NotifyOnValidationError,
					Converter = binding.Converter,
					ConverterParameter = binding.ConverterParameter,
					ConverterCulture = binding.ConverterCulture,
					IsAsync = binding.IsAsync,
					AsyncState = binding.AsyncState,
					Mode = binding.Mode,
					XPath = binding.XPath,
					ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
					ValidatesOnNotifyDataErrors = binding.ValidatesOnNotifyDataErrors,
					BindsDirectlyToSource = binding.BindsDirectlyToSource,
					ValidatesOnExceptions = binding.ValidatesOnExceptions,
					Path = binding.Path,
					UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter
				};
			foreach (var validationRule in binding.ValidationRules)
			{
				result.ValidationRules.Add(validationRule);
			}
			result.XPath = binding.XPath;

			return result;
		}

		public static object Eval(this Binding binding, DependencyObject dependencyObject = null)
		{
			dependencyObject = dependencyObject ?? new DependencyObject();
			BindingOperations.SetBinding(dependencyObject, DummyProperty, binding);
			return dependencyObject.GetValue(DummyProperty);
		}
	}

	public class MillerColumnColumn : ListBox
	{
		static MillerColumnColumn()
		{
			
		}
		public MillerColumnColumn()
		{
			
		}
	}
}
