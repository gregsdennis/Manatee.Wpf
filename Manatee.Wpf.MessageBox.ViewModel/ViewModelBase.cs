﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Manatee.Wpf.MessageBox.ViewModel
{
	/// <summary>
	/// A base class that implements the infrastructure for property change notification and automatically performs UI thread marshalling.
	/// </summary>
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		private bool _isNotifying;

		/// <summary>
		/// Creates an instance of <see cref = "ViewModelBase" />.
		/// </summary>
		protected ViewModelBase()
		{
			_isNotifying = true;
		}

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public virtual event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Enables/Disables property change notification.
		/// Virtualized in order to help with document oriented view models.
		/// </summary>
		public virtual bool IsNotifying
		{
			get { return _isNotifying; }
			set
			{
				_isNotifying = value;
				Refresh();
			}
		}

		/// <summary>
		/// Raises a change notification indicating that all bindings should be refreshed.
		/// </summary>
		public virtual void Refresh()
		{
			NotifyOfPropertyChange(string.Empty);
		}

		/// <summary>S
		/// Notifies subscribers of the property change.
		/// </summary>
		/// <param name = "propertyName">Name of the property.</param>
		[NotifyPropertyChangedInvocator]
		public virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
		{
			if (!IsNotifying || PropertyChanged == null) return;

			PlatformProvider.Current?.OnUiThread(() => _OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
		}

		/// <summary>
		/// Raises the specified event.
		/// </summary>
		protected void RaiseEvent(EventHandler action, EventArgs args = null)
		{
			action?.Invoke(this, args);
		}

		/// <summary>
		/// Raises the specified event.
		/// </summary>
		protected void RaiseEvent<T>(EventHandler<T> action, T args = default(T))
		{
			action?.Invoke(this, args);
		}

		private void _OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}
	}
}
