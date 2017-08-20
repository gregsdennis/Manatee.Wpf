using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using Manatee.Ui.Mvvmc;

namespace Manatee.Ui.Wpf
{
	/// <summary>
	/// A caching subsystem for <see cref="AssemblySource"/>.
	/// </summary>
	internal static class AssemblySourceCache
	{
		private static bool _isInstalled;
		private static readonly IDictionary<string, Type> _typeNameCache = new Dictionary<string, Type>();

		/// <summary>
		/// Installs the caching subsystem.
		/// </summary>
		public static void Install()
		{
			if (_isInstalled) return;
			_isInstalled = true;

			var directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
			var assemblies = Directory.GetFiles(directory, "*.dll")
			                          .Select(s => _TryRun(() => Assembly.LoadFile(s), s))
			                          .Where(a => _TryRun(() => a != null && a.GetCustomAttributes().OfType<ViewContainerAttribute>().Any(), a.Location))
									  .ToList();
			assemblies.Insert(0, Assembly.GetEntryAssembly());
			AssemblySource.Instance.AddRange(assemblies);

			AssemblySource.Instance.CollectionChanged += (s, e) =>
				{
					switch (e.Action)
					{
						case NotifyCollectionChangedAction.Add:
							e.NewItems
							 .OfType<Assembly>()
							 .SelectMany(a => a.GetExportedTypes())
							 .Apply(t => _typeNameCache[t.Name] = t);
							break;
						case NotifyCollectionChangedAction.Remove:
						case NotifyCollectionChangedAction.Replace:
						case NotifyCollectionChangedAction.Reset:
							_typeNameCache.Clear();
							AssemblySource.Instance
							              .SelectMany(a => a.GetExportedTypes())
							              .Apply(t => _typeNameCache[t.Name] = t);
							break;
					}
				};
			AssemblySource.Instance.Refresh();
			AssemblySource.FindTypeByNames = names =>
				{
					var type = names?.Select(n => _typeNameCache.GetValueOrDefault(n)).FirstOrDefault(t => t != null);
					return type;
				};
		}

		private static T _TryRun<T>(Func<T> action, string s)
		{
			try
			{
				return action();
			}
			catch (Exception)
			{
				typeof(AssemblySourceCache).Log().Warn($"Could not load assembly '{s}' while checking for available views.  Skipping...");
				return default(T);
			}
		}
	}
}