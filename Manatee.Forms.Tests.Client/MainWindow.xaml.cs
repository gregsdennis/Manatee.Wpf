using System.Windows;
using Manatee.Wpf.Forms.ViewModel;
using Manatee.Wpf.Forms.ViewModel.Validation;

namespace Manatee.Forms.Tests.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public FormViewModel DataSource { get; }

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

			InitializeComponent();
		}
	}
}
