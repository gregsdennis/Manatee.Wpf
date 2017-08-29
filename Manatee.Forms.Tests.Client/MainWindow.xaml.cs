using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Manatee.Wpf.Forms.ViewModel;
using Manatee.Wpf.Forms.ViewModel.Validation;
using Manatee.Wpf.MessageBox;
using Manatee.Wpf.MessageBox.ViewModel;

namespace Manatee.Wpf.Tests.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public FormViewModel DataSource { get; }
		public MessageBoxViewModel MessageBoxData { get; }

		public bool IsMouseOverTextVisible
		{
			get { return (bool)GetValue(IsMouseOverTextVisibleProperty); }
			set { SetValue(IsMouseOverTextVisibleProperty, value); }
		}

		public static readonly DependencyProperty IsMouseOverTextVisibleProperty =
			DependencyProperty.Register("IsMouseOverTextVisible", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

		public string DummyStringValue
		{
			get { return (string)GetValue(DummyStringValueProperty); }
			set { SetValue(DummyStringValueProperty, value); }
		}

		// Using a DependencyProperty as the backing store for DummyStringValue.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DummyStringValueProperty =
			DependencyProperty.Register("DummyStringValue", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

		public ICommand ShowMessageBox { get; }
		public ICommand ShowMouseOverText { get; }

		public MainWindow()
		{
			DataSource = new FormViewModel
				{
					Title = "Here's a form",
					Sections =
						{
							new FieldSection
								{
									//Title = "Section 1",
									Fields =
										{
											new NumericField
												{
													Label = "Enter a number:",
													StringFormat = "C"
												},
											new StringField
												{
													Label = "Full name:",
													ValidationRules = {new StringMinLengthRule(10, true)}
												},
											new SelectorField<string>
												{
													Label = "Select one:",
													Options =
														{
															"option 1",
															"option 2",
															"option 3"
														}
												}
										}
								},
							new FieldSection
								{
									Title = "Additional Information",
									Fields =
										{
											new ToggleField(false)
												{
													Label = "Check this box:",
													ValidationRules =
														{
															new RequiredValueRule<bool>()
														}
												},
											new StringField
												{
													Label = "Enter some text:",
													ValidationRules =
														{
															new StringMinLengthRule(10, true),
															new StringPatternRule("^[a-z]+$")
														}
												},
											new DateField
												{
													Label = "Start date:"
												}
										}
								}
						}
			};
			DataSource.AcceptRequested += (sender, args) => Close();
			DataSource.CancelRequested += (sender, args) => Close();

			MessageBoxData = new MessageBoxViewModel
				{
					Title = "Title",
					Message = "This is some text in the message box.",
					Icon = MessageBoxIcon.Info,
					ShowConfirm = true,
					ConfirmText = "OK",
					ShowDecline = false,
					DeclineText = "No",
					ShowCancel = true,
					CancelText = "Cancel",
					DefaultAction = MessageBoxAction.Confirm,
					AllowNonResponse = false
				};
			ShowMessageBox = new SimpleCommand(_ShowMessage);
			ShowMouseOverText = new SimpleCommand(_ShowMouseOverText);

			InitializeComponent();
		}

		private async void _ShowMouseOverText()
		{
			IsMouseOverTextVisible = true;
			await Task.Delay(3000);
			IsMouseOverTextVisible = false;
		}

		private void _ShowMessage()
		{
			var vm = new MessageBoxViewModel
				{
					Title = MessageBoxData.Title,
					Message = MessageBoxData.Message,
					Icon = MessageBoxData.Icon,
					ShowConfirm = MessageBoxData.ShowConfirm,
					ConfirmText = MessageBoxData.ConfirmText,
					ShowDecline = MessageBoxData.ShowDecline,
					DeclineText = MessageBoxData.DeclineText,
					ShowCancel = MessageBoxData.ShowCancel,
					CancelText = MessageBoxData.CancelText,
					DefaultAction = MessageBoxData.DefaultAction,
					AllowNonResponse = MessageBoxData.AllowNonResponse
				};

			var view = new MessageBoxView
				{
					DataContext = vm,
					WindowStartupLocation = WindowStartupLocation.CenterOwner,
					Owner = this
				};

			view.ShowDialog();
		}
	}
}
