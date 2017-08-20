using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace Manatee.Ui.Wpf
{
	/// <summary>
	/// Allows binding events to ICommand properties.
	/// </summary>
	public class EventBinding : MarkupExtension
	{
		private class BindingTarget : FrameworkElement
		{
			public static readonly DependencyProperty CommandProperty =
				DependencyProperty.Register("Command", typeof(ICommand), typeof(BindingTarget), new PropertyMetadata(null));

			public ICommand Command
			{
				get { return (ICommand)GetValue(CommandProperty); }
				set { SetValue(CommandProperty, value); }
			}
		}

		private class EventCommandHandler
		{
			private readonly ICommand _command;

			public EventCommandHandler(ICommand command)
			{
				_command = command ?? throw new ArgumentNullException(nameof(command), "This may have occurred because either the data context or the command property is null.");
			}

			public void Handle<T>(object sender, T e)
			{
				if (_command.CanExecute(e))
					_command.Execute(e);
			}
		}

		private readonly string _commandName;

		/// <summary>
		/// Creates a new instance of the <see cref="EventBinding"/> class.
		/// </summary>
		/// <param name="commandName"></param>
		public EventBinding(string commandName)
		{
			_commandName = commandName ?? throw new ArgumentNullException(nameof(commandName));
		}

		/// <summary>When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension. </summary>
		/// <returns>The object value to set on the property where the extension is applied. </returns>
		/// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var provider = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

			var target = provider.TargetObject as FrameworkElement;
			if (target == null)
				throw new ArgumentException("Event bindings can only be set on types derived from FrameworkElement.");

			var handledEvent = provider.TargetProperty as EventInfo;
			Type eventType;
			if (handledEvent == null)
			{
				// Some events are handled via Add*Handler methods on other classes.
				// Examples of this are MouseEnter (Mouse.AddMouseEnterHandler) and LostFocus (FocusManager.AddLostFocusHandler).
				// In these cases, the event handler type is the second parameter in that method.
				var handledMethod = provider.TargetProperty as MethodInfo;
				if (handledMethod == null)
					throw new ArgumentException("Event bindings can only be set on events.");
				eventType = handledMethod.GetParameters()
				                         .Last()
				                         .ParameterType;
			}
			else
				eventType = handledEvent.EventHandlerType;
			var eventArgsType = eventType.GetMethod("Invoke")
										 .GetParameters()
										 .Last()
										 .ParameterType;

			var tempTarget = new BindingTarget { DataContext = target.DataContext };
			var binding = new Binding(_commandName);
			BindingOperations.SetBinding(tempTarget, BindingTarget.CommandProperty, binding);

			var command = tempTarget.Command;

			var handler = new EventCommandHandler(command);
			var handlerMethod = typeof(EventCommandHandler).GetMethod(nameof(handler.Handle))
														   .MakeGenericMethod(eventArgsType);

			return Delegate.CreateDelegate(eventType, handler, handlerMethod);
		}
	}
}
