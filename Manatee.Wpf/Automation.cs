using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Manatee.Wpf
{
	/// <summary>
	///     Automatically generates automation IDs for controls.
	/// </summary>
	public static class Automation
	{
		/// <summary>
		///     Tracks whether a control will have an Automation ID generated for it.
		/// </summary>
		public static readonly DependencyProperty AutoProperty;

		/// <summary>
		///     If the Automation ID generation is turned on, this will override whatever value it would've normally generated.
		/// </summary>
		public static readonly DependencyProperty AutomationIdOverrideProperty;

		static Automation()
		{
			AutoProperty = DependencyProperty.RegisterAttached("Auto", typeof(bool), typeof(Automation), new PropertyMetadata(false, _OnAutoChanged));
			AutomationIdOverrideProperty = DependencyProperty.RegisterAttached("AutomationIdOverride", typeof(string), typeof(Automation),
			                                                                   new PropertyMetadata(string.Empty, _OnAutomationIdOverrideChange));
		}

		private static void _OnAutomationIdOverrideChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(e.NewValue.ToString()))
				d.SetValue(AutomationProperties.AutomationIdProperty, e.NewValue);
		}

		/// <summary>
		///     Gets whether a control will have an Automation ID generated for it.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool GetAuto(DependencyObject obj)
		{
			return (bool) obj.GetValue(AutoProperty);
		}

		/// <summary>
		///     Sets whether a control will have an Automation ID generated for it.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="value"></param>
		public static void SetAuto(DependencyObject obj, bool value)
		{
			obj.SetValue(AutoProperty, value);
		}

		/// <summary>
		///     Gets the value that will override the Automation ID if it is set.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string GetAutomationIdOverride(DependencyObject obj)
		{
			return (string) obj.GetValue(AutomationIdOverrideProperty);
		}

		/// <summary>
		///     Overrides the default-generated Automation ID, if auto-generation is turned on.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="value"></param>
		public static void SetAutomationIdOverride(DependencyObject obj, string value)
		{
			obj.SetValue(AutomationIdOverrideProperty, value);
		}

		private static void _OnAutoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!(bool) e.NewValue)
			{
				d.ClearValue(AutomationProperties.AutomationIdProperty);
				return;
			}
			if (!string.IsNullOrWhiteSpace(AutomationProperties.GetAutomationId(d)))
				return;
			var func = _GetGenerationMethod(d);
			var text = func(d);
			if (text == null)
			{
				d.ClearValue(AutomationProperties.AutomationIdProperty);
				return;
			}
			AutomationProperties.SetAutomationId(d, text);
		}

		private static Func<DependencyObject, string> _GetGenerationMethod(DependencyObject d)
		{
			if (d is IAutomate || d is TextBox || d is TextBlock || d is ButtonBase || d is ItemsControl || d is PasswordBox)
				return _GenerateIdFromBoundPropertyPath;
			return _GenerateIdFromType;
		}

		private static string _GenerateIdFromType(DependencyObject d)
		{
			return d.GetType().Name;
		}

		private static string _GenerateIdFromBoundPropertyPath(DependencyObject d)
		{
			var dependencyProperty = _GetDependencyProperty(d);
			if (dependencyProperty != null)
				_WaitForBindingAndSet(dependencyProperty, d);
			return null;
		}

		private static DependencyProperty _GetDependencyProperty(DependencyObject d)
		{
			switch (d)
			{
				case IAutomate automate:
					return automate.AutomationProperty;
				case TextBox _:
					return TextBox.TextProperty;
				case TextBlock _:
					return TextBlock.TextProperty;
				case ToggleButton _:
					return ToggleButton.IsCheckedProperty;
				case ButtonBase _:
					return ButtonBase.CommandProperty;
				case ItemsControl _:
					return ItemsControl.ItemsSourceProperty;
				default:
					return null;
			}
		}

		private static void _WaitForBindingAndSet(DependencyProperty dp, DependencyObject d)
		{
			var frameworkElement = d as FrameworkElement;
			EventHandler handler = (sender, args) =>
				{
					if (!string.IsNullOrWhiteSpace(GetAutomationIdOverride(d))) return;
					var binding = BindingOperations.GetBinding(d, dp);
					if (binding == null)
					{
						d.ClearValue(AutomationProperties.AutomationIdProperty);
						return;
					}
					if (!string.IsNullOrWhiteSpace(AutomationProperties.GetAutomationId(d))) return;
					AutomationProperties.SetAutomationId(d, binding.Path.Path);
				};
			DependencyPropertyDescriptor.FromProperty(dp, d.GetType()).AddValueChanged(d, handler);
			if (frameworkElement != null)
				frameworkElement.Unloaded += (sender, args) => DependencyPropertyDescriptor.FromProperty(dp, d.GetType()).RemoveValueChanged(d, handler);
		}
	}
}