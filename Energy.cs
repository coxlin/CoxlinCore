using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoxlinCore
{
    public class Energy : MonoBehaviour
    {
        [SerializeField]
        private float _max;
        public float Max => _max;

        [SerializeField]
        private float _rechargeSpeed;
        public float Current { private set; get; }
        public bool Recharging { private set; get; }

        public float Normalized => Current / Max;

        public void SetCurrent(float val)
        {
            Current = Mathf.Min(_max, val);
            Current = Mathf.Max(Current, 0f);
            if (Current <= 0f)
            {
                Recharge();
            }
        }

        public void Recharge()
        {
            if (!Recharging)
            {
                StartCoroutine(RechargeCo());
            }
        }

        private IEnumerator RechargeCo()
        {
            Recharging = true;
            while (Current < _max)
            {
                yield return null;
                SetCurrent(Current + (_rechargeSpeed * Time.deltaTime));
            }
            Current = _max;
            Recharging = false;
        }
    }
}
