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

		public ICommand ShowMessageBox { get; }

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

			InitializeComponent();
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
