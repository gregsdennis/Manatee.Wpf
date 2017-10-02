using System;
using System.ComponentModel;
using System.Windows;
using Manatee.Wpf.MessageBox.ViewModel;

namespace Manatee.Wpf.MessageBox
{
	/// <summary>
	/// Interaction logic for MessageBoxView.xaml
	/// </summary>
	public partial class MessageBoxView : Window
	{
		static MessageBoxView()
		{
			PlatformProvider.Initialize();
		}
		public MessageBoxView()
		{
			InitializeComponent();

			DataContextChanged += _OnDataContextChanged;
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			IconHelper.RemoveIcon(this);
			base.OnSourceInitialized(e);
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			var content = DataContext as MessageBoxViewModel;
			e.Cancel = !content?.CanClose() ?? false;

			base.OnClosing(e);
		}

		private void _OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var oldContent = e.OldValue as MessageBoxViewModel;
			if (oldContent != null)
				oldContent.Close -= _Close;
			var newContent = e.NewValue as MessageBoxViewModel;
			if (newContent != null)
				newContent.Close += _Close;
		}

		private void _Close(object sender, EventArgs e)
		{
			Close();
		}
	}
}
