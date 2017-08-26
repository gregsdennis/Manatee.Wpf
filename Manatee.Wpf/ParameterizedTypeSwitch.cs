using System;
using System.Collections.Generic;

namespace Manatee.Wpf
{
	internal class ParameterizedTypeSwitch<T>
	{
		private readonly Dictionary<Type, Action<T>> _actions;

		public ParameterizedTypeSwitch()
		{
			_actions = new Dictionary<Type, Action<T>>();
		}

		public Action<T> Default { get; set; }

		public bool CheckInheritance { get; set; }

		public void Case<TParam>(Action<TParam> action) where TParam : T
		{
			var typeFromHandle = typeof(TParam);
			_actions[typeFromHandle] = delegate (T r) { action((TParam)(object)r); };
		}

		public bool TryInvoke(T parameter)
		{
			var type = parameter.GetType();
			while (CheckInheritance && type != typeof(T).BaseType && !_actions.ContainsKey(type))
				type = type.BaseType;
			if (!(type == null) && _actions.ContainsKey(type))
			{
				_actions[type](parameter);
				return true;
			}
			if (Default != null)
			{
				Default(parameter);
				return true;
			}
			return false;
		}

		public void Invoke(T parameter)
		{
			var type = parameter.GetType();
			while (CheckInheritance && type != typeof(T).BaseType && !_actions.ContainsKey(type))
				type = type.BaseType;
			if (!(type == null) && _actions.ContainsKey(type))
			{
				_actions[type](parameter);
				return;
			}
			if (Default != null)
			{
				Default(parameter);
				return;
			}
			throw new ArgumentOutOfRangeException($"Type {type} has not been handled.");
		}
	}

	public class ParameterizedTypeSwitch<T, TReturn>
	{
		private readonly Dictionary<Type, Func<T, TReturn>> _actions;

		public Func<T, TReturn> Default { get; set; }

		public bool CheckInheritance { get; set; }

		public ParameterizedTypeSwitch()
		{
			_actions = new Dictionary<Type, Func<T, TReturn>>();
		}

		public void Case<TParam>(Func<TParam, TReturn> action) where TParam : T
		{
			var typeFromHandle = typeof(TParam);
			_actions[typeFromHandle] = r => action((TParam) (object) r);
		}

		public bool TryInvoke(T parameter, out TReturn retVal)
		{
			var type = parameter.GetType();
			while (CheckInheritance && type != typeof(T).BaseType && !_actions.ContainsKey(type))
				type = type.BaseType;
			if (!(type == null) && _actions.ContainsKey(type))
			{
				retVal = _actions[type](parameter);
				return true;
			}
			if (Default != null)
			{
				retVal = Default(parameter);
				return true;
			}
			retVal = default(TReturn);
			return false;
		}

		public TReturn Invoke(T parameter)
		{
			var type = parameter.GetType();
			while (CheckInheritance && type != typeof(T).BaseType && !_actions.ContainsKey(type))
				type = type.BaseType;
			if (!(type == null) && _actions.ContainsKey(type)) return _actions[type](parameter);
			if (Default != null) return Default(parameter);
			throw new ArgumentOutOfRangeException($"Type {type} has not been handled.");
		}
	}
}