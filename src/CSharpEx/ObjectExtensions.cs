#nullable enable
using System;

namespace CSharpEx
{
	public static class ObjectExtensions
	{
		/// <summary>
		/// Intenta convertir un objeto a cualquier tipo (T), manejando conversiones numéricas y de punteros.
		/// </summary>
		public static T To<T>(this object? obj)
		{
			if (obj == null)
				return default!;

			Type targetType = typeof(T);

			if (obj.GetType() == targetType)
			{
				return (T)obj;
			}

			if (targetType == typeof(UIntPtr))
			{
				return (T)(object)new UIntPtr(Convert.ToUInt64(obj));
			}
			if (targetType == typeof(IntPtr))
			{
				return (T)(object)new IntPtr(Convert.ToInt64(obj));
			}

			try
			{
				return (T)Convert.ChangeType(obj, targetType);
			}
			catch
			{
				return (T)obj;
			}
		}

		public static T? As<T>(this object? obj) where T : class
		{
			return obj as T;
		}
	}
}