using System;
using System.Linq;

namespace Manatee.Wpf.Forms
{
	public static class TypeExtensions
	{
		public static bool InheritsOrImplements(this Type child, Type parent)
		{
			parent = _ResolveGenericTypeDefinition(parent);

			var currentChild = child.IsGenericType
				                   ? child.GetGenericTypeDefinition()
				                   : child;

			while (currentChild != typeof(object))
			{
				if (parent == currentChild || _HasAnyInterfaces(parent, currentChild))
					return true;

				currentChild = currentChild.BaseType != null
				               && currentChild.BaseType.IsGenericType
					               ? currentChild.BaseType.GetGenericTypeDefinition()
					               : currentChild.BaseType;

				if (currentChild == null)
					return false;
			}
			return false;
		}

		private static bool _HasAnyInterfaces(Type parent, Type child)
		{
			return child.GetInterfaces()
			            .Any(childInterface =>
				            {
					            var currentInterface = childInterface.IsGenericType
						                                   ? childInterface.GetGenericTypeDefinition()
						                                   : childInterface;

					            return currentInterface == parent;
				            });
		}

		private static Type _ResolveGenericTypeDefinition(Type parent)
		{
			bool shouldUseGenericType = !(parent.IsGenericType && parent.GetGenericTypeDefinition() != parent);

			if (parent.IsGenericType && shouldUseGenericType)
				parent = parent.GetGenericTypeDefinition();
			return parent;
		}
	}
}