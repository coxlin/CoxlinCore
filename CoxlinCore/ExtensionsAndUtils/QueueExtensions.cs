/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/
using System.Collections.Generic;

namespace CoxlinCore
{
    public static class QueueExtensions
    {
        public static void EnqueueAll<T>(this Queue<T> q, ICollection<T> collection)
        {
            foreach (var c in collection)
            {
                q.Enqueue(c);
            }
        }
    }
}