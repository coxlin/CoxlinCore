using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoxlinCore
{

    public abstract class AttackCollider : MonoBehaviour
    {
        protected abstract DamageRecieverSystem DamageRecieverSystem { get; }
        private readonly List<int> _objectsInCollider = new List<int>();

        [SerializeField]
        private string[] _tagsToIgnore;

        public Collider2D Collider { private set; get; }
        public Direction Direction;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Array.Exists(_tagsToIgnore, x => x == collision.tag))
            {
                return;
            }
            _objectsInCollider.Add(collision.gameObject.GetInstanceID());
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (Array.Exists(_tagsToIgnore, x => x == collision.tag))
            {
                return;
            }
            _objectsInCollider.Remove(collision.gameObject.GetInstanceID());
        }

        public void DamageAll(float val)
        {
            foreach (var id in _objectsInCollider)
            {
                DamageRecieverSystem.TakeDamage(id, val);
            }
        }
    }
}
