using System.Windows;
using System.Windows.Controls;
using Manatee.Wpf.Forms.ViewModel;

namespace Manatee.Wpf.Forms
{
	public class FieldTemplateSelector : DataTemplateSelector
	{
		public static FieldTemplateSelector Instance { get; }

		static FieldTemplateSelector()
		{
			Instance = new FieldTemplateSelector();
		}
		private FieldTemplateSelector() { }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item == null) return base.SelectTemplate(null, container);

			var type = item.GetType();
			if (type.InheritsOrImplements(typeof(SelectorField<>)))
				return Application.Current.FindResource("SelectorTemplate") as DataTemplate;

			return base.SelectTemplate(null, container);
		}
	}
}
