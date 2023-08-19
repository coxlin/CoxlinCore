/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/
using System.Collections.Generic;

namespace CoxlinCore
{
    public static class HashsetExtensions
    {
        /// <summary>
        /// This does a foreach through all the elements
        /// and alternative is to use IsSubsetOf which may be
        /// faster depending on the size of your hashset
        /// However, this has been implemented using the foreach method to account for HUGE hashsets
        /// </summary>
        /// <param name="hashset"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool ContainsAll<T>(this HashSet<T> hashset, ICollection<T> collection)
        {
            foreach (var item in collection)
            {
                if (!hashset.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }
    }
}