namespace CSharpEx
{
	public static class GuardExtensions
	{
		public static bool IsBlank(this string? s)
			=> string.IsNullOrWhiteSpace(s);

		public static bool HasContent(this string? s)
			=> !string.IsNullOrWhiteSpace(s);

		public static int Clamp(this int value, int min, int max)
			=> Math.Max(min, Math.Min(max, value));
	}
}