﻿using System.Threading.Tasks;

namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Denotes an object that can be closed.
	/// </summary>
	public interface IClose
	{
		/// <summary>
		/// Tries to close this instance.
		/// Also provides an opportunity to pass a dialog result to it's corresponding view.
		/// </summary>
		Task TryClose();
	}
}