using UnityEngine;
using System.Collections;

namespace CoxlinCore
{
    public sealed class Health : MonoBehaviour
    {
        private float _current;
        public float GetCurrent() => _current;

        [SerializeField]
        private float _max;
        public float GetMax() => _max;
        public bool Alive => _current > 0f;

        public float Normalized => Mathf.Max(0f, _current / _max);

        public void SetCurrent(float val)
        {
            _current = Mathf.Min(val, _max);
            _current = Mathf.Max(_current, 0f);
        }

        public void AliveCheck(System.Action onDie)
        {
            StartCoroutine(AliveCo(onDie));
        }

        private IEnumerator AliveCo(System.Action onDie)
        {
            while (Alive)
            {
                yield return null;
            }
            onDie();
        }
    }
}
