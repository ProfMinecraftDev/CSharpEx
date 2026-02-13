namespace CSharpEx
{
	public static class NumberExtensions
	{
		/// <summary>
		/// Convierte una cantidad de bytes en un formato legible (KB, MB, GB, etc.)
		/// </summary>
		public static string ToSizeString(this long bytes)
		{
			string[] suffix = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
			double readable = bytes;
			int order = 0;

			while (readable >= 1024 && order < suffix.Length - 1)
			{
				order++;
				readable /= 1024;
			}

			return $"{readable:0.##} {suffix[order]}";
		}
	}
}