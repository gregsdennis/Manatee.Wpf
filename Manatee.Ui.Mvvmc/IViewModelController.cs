namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// Controls lifetime and manages activities for a view model
	/// </summary>
	/// <typeparam name="T">The type of view model.</typeparam>
	public interface IViewModelController<out T>
		where T : ViewModelBase
	{
		/// <summary>
		/// Gets the view model.
		/// </summary>
		T GetModel();
	}

	/// <summary>
	/// Controls lifetime and manages activities for a view model which require context.
	/// </summary>
	/// <typeparam name="TViewModel"></typeparam>
	/// <typeparam name="TContext"></typeparam>
	public interface IViewModelController<out TViewModel, in TContext>
		where TViewModel : ViewModelBase
	{
		/// <summary>
		/// Gets the view model.
		/// </summary>
		/// <param name="context">The context for the view model.</param>
		TViewModel GetModel(TContext context);
	}
}
