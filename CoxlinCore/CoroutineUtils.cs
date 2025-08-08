using System;
using System.Collections;
using UnityEngine;

namespace CoxlinCore
{
    public static class CoroutineUtils
    {
        public static void WaitForSecondsAndPerformAction(
            MonoBehaviour mb,
            float seconds, 
            System.Action action)
        {
            mb.StartCoroutine(WaitAndDoActionCoroutine(seconds, action));
        }

        private static IEnumerator WaitAndDoActionCoroutine(float seconds, System.Action action)
        {
            yield return WaitForSeconds(seconds);
            action();
        }
        
        public static IEnumerator WaitUntil(Func<bool> isTrue)
        {
            while (!isTrue())
            {
                yield return null;
            }
        }

        public static IEnumerator WaitForSeconds(float secs)
        {
            float start = Time.time;
            while(Time.time <= start + secs)
            {
                yield return null;
            }
        }
        
        public static IEnumerator WaitForSecondsRealtime(float secs)
        {
            float start = Time.realtimeSinceStartup;
            while (Time.realtimeSinceStartup <= start + secs)
            {
                yield return null;
            }
        }
    }
}