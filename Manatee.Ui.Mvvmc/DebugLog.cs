using System;
using System.Diagnostics;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	///   A simple logger thats logs everything to the debugger.
	/// </summary>
	public class DebugLog : ILog
	{
		/// <summary>
		/// A singleton instance of <see cref="DebugLog"/>.
		/// </summary>
		public static DebugLog Instance { get; }

		static DebugLog()
		{
			Instance = new DebugLog();
		}
		private DebugLog() {}

		/// <summary>
		/// Logs the message as info.
		/// </summary>
		/// <param name="format">A formatted message.</param>
		/// <param name="args">Parameters to be injected into the formatted message.</param>
		public void Info(string format, params object[] args)
		{
			Debug.WriteLine($"INFO: {string.Format(format, args)}");
		}

		/// <summary>
		/// Logs the message as a warning.
		/// </summary>
		/// <param name="format">A formatted message.</param>
		/// <param name="args">Parameters to be injected into the formatted message.</param>
		public void Warn(string format, params object[] args)
		{
			Debug.WriteLine($"WARN: {string.Format(format, args)}");
		}

		/// <summary>
		/// Logs the exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		public void Error(Exception exception)
		{
			Debug.WriteLine($"ERROR: {exception}");
		}
	}
}
