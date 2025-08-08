using UnityEngine;

namespace CoxlinCore
{
    public abstract class LazySingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject
                    {
                        name = typeof(T).ToString()
                    };
                    _instance = go.AddComponent<T>();
                }
                return _instance;
            }
        }

    }
}