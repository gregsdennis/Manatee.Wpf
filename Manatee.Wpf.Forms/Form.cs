using System.Windows;
using System.Windows.Controls;
using Manatee.Wpf.Forms.ViewModel;

namespace Manatee.Wpf.Forms
{
	public class Form : Control
	{
		public int Columns
		{
			get { return (int)GetValue(ColumnsProperty); }
			set { SetValue(ColumnsProperty, value); }
		}

		public static readonly DependencyProperty ColumnsProperty =
			DependencyProperty.Register("Columns", typeof(int), typeof(Form), new PropertyMetadata(1));

		public FormViewModel FormDataSource
		{
			get { return (FormViewModel)GetValue(FormDataSourceProperty); }
			set { SetValue(FormDataSourceProperty, value); }
		}

		// Using a DependencyProperty as the backing store for FormDataSource.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty FormDataSourceProperty =
			DependencyProperty.Register("FormDataSource", typeof(FormViewModel), typeof(Form), new PropertyMetadata(null));

		static Form()
		{
			PlatformProvider.Initialize();

			DefaultStyleKeyProperty.OverrideMetadata(typeof(Form), new FrameworkPropertyMetadata(typeof(Form)));
		}
	}
}
