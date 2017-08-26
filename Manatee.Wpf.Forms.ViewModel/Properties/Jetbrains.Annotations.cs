using System;

// ReSharper disable once CheckNamespace
namespace JetBrains.Annotations
{
	/// <summary>
	/// Indicates that parameter is regular expression pattern.
	/// </summary>
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class RegexPatternAttribute : Attribute { }

	/// <summary>
	/// Indicates that the method is contained in a type that implements
	/// <c>System.ComponentModel.INotifyPropertyChanged</c> interface and this method
	/// is used to notify that some property value changed.
	/// </summary>
	/// <remarks>
	/// The method should be non-static and conform to one of the supported signatures:
	/// <list>
	/// <item><c>NotifyChanged(string)</c></item>
	/// <item><c>NotifyChanged(params string[])</c></item>
	/// <item><c>NotifyChanged{T}(Expression{Func{T}})</c></item>
	/// <item><c>NotifyChanged{T,U}(Expression{Func{T,U}})</c></item>
	/// <item><c>SetProperty{T}(ref T, T, string)</c></item>
	/// </list>
	/// </remarks>
	/// <example><code>
	/// public class Foo : INotifyPropertyChanged {
	///   public event PropertyChangedEventHandler PropertyChanged;
	/// 
	///   [NotifyPropertyChangedInvocator]
	///   protected virtual void NotifyChanged(string propertyName) { ... }
	///
	///   string _name;
	/// 
	///   public string Name {
	///     get { return _name; }
	///     set { _name = value; NotifyChanged("LastName"); /* Warning */ }
	///   }
	/// }
	/// </code>
	/// Examples of generated notifications:
	/// <list>
	/// <item><c>NotifyChanged("Property")</c></item>
	/// <item><c>NotifyChanged(() =&gt; Property)</c></item>
	/// <item><c>NotifyChanged((VM x) =&gt; x.Property)</c></item>
	/// <item><c>SetProperty(ref myField, value, "Property")</c></item>
	/// </list>
	/// </example>
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		public NotifyPropertyChangedInvocatorAttribute() { }
		public NotifyPropertyChangedInvocatorAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}

		public string ParameterName { get; private set; }
	}

	/// <summary>
	/// Indicates that the value of the marked element could never be <c>null</c>.
	/// </summary>
	/// <example><code>
	/// [NotNull] object Foo() {
	///   return null; // Warning: Possible 'null' assignment
	/// }
	/// </code></example>
	[AttributeUsage(
		AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
		AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event |
		AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.GenericParameter)]
	internal sealed class NotNullAttribute : Attribute { }
}
