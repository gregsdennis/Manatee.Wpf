using System;
using System.Windows;
using System.Windows.Controls;
using Manatee.Wpf.Forms.ViewModel;
using System.Collections.Generic;

namespace Manatee.Wpf.Forms
{
	public class FieldTemplateSelector : DataTemplateSelector
	{
		public static FieldTemplateSelector Instance { get; }

		public Dictionary<Type, string> ResourceMap { get; }

		static FieldTemplateSelector()
		{
			Instance = new FieldTemplateSelector();
		}

		private FieldTemplateSelector()
		{
			ResourceMap = new Dictionary<Type, string>();
		}

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			if (item == null) return base.SelectTemplate(null, container);

			var type = item.GetType();

			if (ResourceMap.TryGetValue(type, out var resourceKey))
				return Application.Current.FindResource(resourceKey) as DataTemplate;

			if (type.InheritsOrImplements(typeof(SelectorField<>)))
				return Application.Current.FindResource("SelectorTemplate") as DataTemplate;

			return base.SelectTemplate(null, container);
		}
	}
}
