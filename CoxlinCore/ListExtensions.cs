/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using System.Collections.Generic;

namespace CoxlinCore
{
    public static class ListExtensions
    {
        /// <summary>
        /// Uses the Fisher-Yates shuffle algorithm
        /// https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            var rand = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                var k = rand.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}