﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Manatee.Wpf.ViewModel
{
	/// <summary>
	/// Enables loosely-coupled publication of and subscription to events.
	/// </summary>
	public class EventAggregator : IEventAggregator
	{
		private class Handler
		{
			private readonly WeakReference _reference;
			private readonly Dictionary<Type, MethodInfo> _supportedHandlers = new Dictionary<Type, MethodInfo>();

			public bool IsDead => _reference.Target == null;

			public Handler(object handler)
			{
				_reference = new WeakReference(handler);

				var interfaces = handler.GetType().GetInterfaces()
										.Where(x => typeof(IHandle).IsAssignableFrom(x) && x.IsGenericType);

				foreach (var @interface in interfaces)
				{
					var type = @interface.GetGenericArguments()[0];
					var method = @interface.GetMethod("Handle", new[] { type });

					if (method != null)
						_supportedHandlers[type] = method;
				}
			}

			public bool Matches(object instance)
			{
				return _reference.Target == instance;
			}

			public bool Handle(Type messageType, object message)
			{
				var target = _reference.Target;
				if (target == null) return false;

				foreach (var pair in _supportedHandlers)
				{
					if (pair.Key.IsAssignableFrom(messageType))
						pair.Value.Invoke(target, new[] { message });
				}

				return true;
			}

			public bool Handles(Type messageType)
			{
				return _supportedHandlers.Any(pair => pair.Key.IsAssignableFrom(messageType));
			}
		}

		private readonly List<Handler> _handlers = new List<Handler>();

		/// <summary>
		/// Searches the subscribed handlers to check if we have a handler for
		/// the message type supplied.
		/// </summary>
		/// <param name="messageType">The message type to check with</param>
		/// <returns>True if any handler is found, false if not.</returns>
		public bool HandlerExistsFor(Type messageType)
		{
			return _handlers.Any(handler => handler.Handles(messageType) & !handler.IsDead);
		}

		/// <summary>
		/// Subscribes an instance to all events declared through implementations of <see cref = "IHandle{T}" />
		/// </summary>
		/// <param name = "subscriber">The instance to subscribe for event publication.</param>
		public virtual void Subscribe(IHandle subscriber)
		{
			if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

			lock (_handlers)
			{
				if (_handlers.Any(x => x.Matches(subscriber))) return;

				_handlers.Add(new Handler(subscriber));
			}
		}

		/// <summary>
		/// Unsubscribes the instance from all events.
		/// </summary>
		/// <param name = "subscriber">The instance to unsubscribe.</param>
		public virtual void Unsubscribe(IHandle subscriber)
		{
			if (subscriber == null) throw new ArgumentNullException(nameof(subscriber));

			lock (_handlers)
			{
				var found = _handlers.FirstOrDefault(x => x.Matches(subscriber));

				if (found != null)
					_handlers.Remove(found);
			}
		}

		/// <summary>
		/// Publishes a message.
		/// </summary>
		/// <param name = "message">The message instance.</param>
		/// <param name = "marshal">Allows the publisher to provide a custom thread marshaller for the message publication.</param>
		public virtual void Publish(object message, Action<Action> marshal = null)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));

			marshal = marshal ?? (action => action());

			Handler[] toNotify;
			lock (_handlers)
			{
				toNotify = _handlers.ToArray();
			}

			marshal(() =>
				        {
					        var messageType = message.GetType();

					        var dead = toNotify.Where(handler => !handler.Handle(messageType, message)).ToList();

					        if (!dead.Any()) return;

					        lock (_handlers)
					        {
						        dead.Apply(x => _handlers.Remove(x));
					        }
				        });
		}
	}
}