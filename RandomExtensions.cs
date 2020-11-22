using System;
using System.Collections.Generic;
using System.Linq;

namespace CoxlinCore
{
    public static class RandomExtensions
    {
        public static T GetRandom<T>(this IList<T> list)
        {
            return GetRandom<T>(list.ToArray());
        }

        public static T GetRandom<T>(this T[] array)
        {
            if (array.Length <= 0)
            {
                return default(T);
            }

            Random rand = new Random();
            return array[rand.Next(0, array.Length - 1)];
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            Random rand = new Random();
            int next = rand.Next();
            return enumerable.OrderBy(x => next);
        }


    }
}
