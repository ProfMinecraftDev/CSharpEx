#nullable enable
using System;

namespace CSharpEx
{
	public static class GuardExtensions
	{
		public static bool IsBlank(this string? s)
		{
			if (s == null)
				return true;

			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
					return false;
			}

			return true;
		}

		public static bool HasContent(this string? s)
			=> !IsBlank(s);

		public static int Clamp(this int value, int min, int max)
			=> Math.Max(min, Math.Min(max, value));
	}
}