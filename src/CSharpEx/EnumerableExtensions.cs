namespace CSharpEx
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
			=> source == null || !source.Any();

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (var item in source)
				action(item);
		}

		public static T PickRandom<T>(this IEnumerable<T> source)
		{
			var list = source as IList<T> ?? source.ToList();
			return list[new Random().Next(list.Count)];
		}

		public static bool AddIfMissing<T>(this IList<T> list, T item)
		{
			if (list.Contains(item))
				return false;
			list.Add(item);
			return true;
		}
	}
}