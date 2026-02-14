#nullable enable
namespace CSharpEx
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Casteo fluido. Ejemplo: myObj.To<string>();
		/// </summary>
		public static T To<T>(this object? obj)
		{
			return (T)obj!;
		}

		/// <summary>
		/// Intenta convertir el objeto. Si falla, devuelve el valor por defecto.
		/// </summary>
		public static T? As<T>(this object? obj) where T : class
		{
			return obj as T;
		}
	}
}