using System.Collections.Generic;
using System.Linq;

namespace CoxlinCore
{
    public static class ListExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static List<T> GetChunk<T>(this List<T> source, int startIndex, int endIndex)
        {
            var list = new List<T>();
            for (int i = startIndex; i < endIndex; ++i)
            {
                list.Add(source[i]);
            }
            return list;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var rand = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
