using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoxlinCore
{
    public class Knockback : MonoBehaviour
    {
        [SerializeField]
        private float _knockbackThrustOnSelf;
        [SerializeField]
        private float _knockbackTimeOnSelf;

        [SerializeField]
        private Rigidbody2D _rb;

        public bool CanKnockback;
        public bool BeingKnockedBack { private set; get; }

        public void DoKnockback()
        {
            if (!CanKnockback || BeingKnockedBack)
            {
                return;
            }
            _rb.gravityScale = 0f;
            StopAllCoroutines();
            _rb.isKinematic = false;
            var diff = _rb.transform.position - transform.position;
            diff = diff.normalized * _knockbackThrustOnSelf;
            _rb.AddForce(diff, ForceMode2D.Impulse);
            BeingKnockedBack = true;
            StartCoroutine(KnockbackCo(_knockbackTimeOnSelf));
        }

        private IEnumerator KnockbackCo(float t)
        {
            yield return CoroutineUtils.WaitForSeconds(t);
            _rb.velocity = Vector2.zero;
            _rb.isKinematic = true;
            BeingKnockedBack = false;
        }
    }

}
