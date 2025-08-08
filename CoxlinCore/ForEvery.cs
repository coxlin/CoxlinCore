using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

namespace CoxlinCore
{
    public static class ForEveryLoops
    {
        public static void ForEvery<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence)
            {
                action(item);
            }
        }

        public static void ForEvery<T>(this IList<T> sequence, Action<T> action)
        {
            int count = sequence.Count;
            for(int i = 0; i < count; ++i)
            {
                action(sequence[i]);
            }
        }

        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        public static void ForEvery<T>(this T[] sequence, System.Action<T> action)
        {
            int count = sequence.Length;
            for (int i = 0; i < count; ++i)
            {
                action(sequence[i]);
            }
        }

        public static void ForEveryKey<K, V>(this Dictionary<K, V> dictionary, Action<K> action)
        {
            foreach (var key in dictionary.Keys)
            {
                action(key);
            }
        }

        public static void ForEveryValue<K, V>(this Dictionary<K, V> dictionary, Action<V> action)
        {
            foreach(var value in dictionary.Values)
            {
                action(value);
            }
        }
    }
}