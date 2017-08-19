namespace Manatee.Ui.Mvvmc
{
	/// <summary>
	/// An <see cref="IConductor"/> that also implements <see cref="IHaveActiveItem"/>.
	/// </summary>
	public interface IConductActiveItem : IConductor, IHaveActiveItem { }
}