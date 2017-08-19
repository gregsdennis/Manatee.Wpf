using System;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Used to manage logging.
	/// </summary>
	public static class LogManager
	{
		private static Func<Type, ILog> _getLog;

		/// <summary>
		/// Creates an <see cref="ILog"/> for the provided type.
		/// </summary>
		public static Func<Type, ILog> GetLog
		{
			get { return _getLog ?? (_getLog = type => NullLog.Instance); }
			set { _getLog = value; }
		}

		private class NullLog : ILog
		{
			public static NullLog Instance { get; }

			static NullLog()
			{
				Instance = new NullLog();
			}
			private NullLog() {}


			public void Info(string format, params object[] args) {}
			public void Warn(string format, params object[] args) {}
			public void Error(Exception exception) {}
		}
	}
}
