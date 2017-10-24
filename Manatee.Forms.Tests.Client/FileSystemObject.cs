using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Manatee.Wpf.Tests.Client
{
	public class FileSystemObject
	{
		private IEnumerable<FileSystemObject> _children;

		public string Name { get; set; }
		public IEnumerable<FileSystemObject> Children
		{
			get
			{
				return _children ?? (_children = _GetChildren());
			}
		}

		private IEnumerable<FileSystemObject> _GetChildren()
		{
			try
			{
				return Directory.GetDirectories(Name ?? @"c:\")
								.Select(d => new FileSystemObject {Name = d}).ToList();
			}
			catch
			{
				return Enumerable.Empty<FileSystemObject>();
			}
		}
	}
}
