using System.Collections;
using UnityEngine;

namespace CoxlinCore
{
    public class DamageFlash : MonoBehaviour
    {
        private bool _flashing = false;
        [SerializeField]
        private float _flashTime = 0.1f;
        [SerializeField]
        private int _flashCount;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public void Flash()
        {
            if (!_flashing)
            {
                StartCoroutine(FlashCo(_flashCount));
            }
        }

        public void ForceStop()
        {
            StopAllCoroutines();
            _flashing = false;
            _spriteRenderer.enabled = true;
        }

        private IEnumerator FlashCo(int count)
        {
            _flashing = true;
            for (int i = 0; i < count; ++i)
            {
                _spriteRenderer.enabled = !_spriteRenderer.enabled;
                yield return CoroutineUtils.WaitForSeconds(_flashTime);
            }
            _flashing = false;
            _spriteRenderer.enabled = true;
        }
    }
}
