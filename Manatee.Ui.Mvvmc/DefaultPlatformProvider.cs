﻿using System;
using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Provides a default implementation.  Will primarily be used for testing.
	/// </summary>
	public class DefaultPlatformProvider : IPlatformProvider
	{
		/// <summary>
		/// A singleton instance of <see cref="DefaultPlatformProvider"/>.
		/// </summary>
		public static DefaultPlatformProvider Instance { get; }

		static DefaultPlatformProvider()
		{
			Instance = new DefaultPlatformProvider();
		}
		private DefaultPlatformProvider() { }

		/// <summary>
		///   Indicates whether or not the framework is in design-time mode.
		/// </summary>
		public bool IsInDesignMode => false;

		/// <summary>
		/// Occurs when the system detects conditions that might change the ability of a command to execute.
		/// </summary>
		public event EventHandler RequerySuggested;

		/// <summary>
		///   Executes the action on the UI thread asynchronously.
		/// </summary>
		/// <param name="action">The action to execute.</param>
		public Task BeginOnUiThread(Func<Task> action)
		{
			return action();
		}

		/// <summary>
		///   Executes the action on the UI thread.
		/// </summary>
		/// <param name = "action">The action to execute.</param>
		public void OnUiThread(Action action)
		{
			action.Invoke();
		}

		/// <summary>
		/// Forces the system to raise the <see cref="IPlatformProvider.RequerySuggested"/> event.
		/// </summary>
		public void InvalidateRequerySuggested()
		{
			RequerySuggested?.Invoke(this, new EventArgs());
		}
	}
}
