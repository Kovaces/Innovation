using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innovation.Models.ExtensionMethods
{
	public static class ShuffleExtensions
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (rng == null) throw new ArgumentNullException("rng");

			return source.ShuffleIterator(rng);
		}

		private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
		{
			var buffer = source.ToList();

			for (var i = 0; i < buffer.Count(); i++)
			{
				var j = rng.Next(i, buffer.Count());
				yield return buffer.ElementAt(j);

				buffer[j] = buffer[i];
			}
		}
	}
	
}
