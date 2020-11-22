using UnityEngine;
using System.Collections.Generic;

namespace CoxlinCore
{
    public abstract class DamageReciever : MonoBehaviour
    {
        protected abstract DamageRecieverSystem DamageRecieverSystem { get; }

        private bool _debugInvincible = false;
        public void SetDebugInvincible(bool enabled) => _debugInvincible = enabled;

        private void Awake()
        {
            DamageRecieverSystem.Register(gameObject.GetInstanceID(), this);
        }

        private void OnDestroy()
        {
            DamageRecieverSystem.Deregister(gameObject.GetInstanceID());
        }

        public void TakeDamage(float val)
        {
            if (_debugInvincible)
            {
                return;
            }
            else
            {
                OnTakeDamage(val);
            }
        }
        
        protected abstract void OnTakeDamage(float val);
    }

    public sealed class DamageRecieverSystem
    {
        private readonly Dictionary<int, DamageReciever> _recieverDic = new Dictionary<int, DamageReciever>();

        public void Register(int uid, DamageReciever damageReciever)
        {
            if (!_recieverDic.ContainsKey(uid))
            {
                _recieverDic.Add(uid, damageReciever);
            }
        }

        public void Deregister(int uid)
        {
            if (_recieverDic.ContainsKey(uid))
            {
                _recieverDic.Remove(uid);
            }
        }

        public void TakeDamage(int uid, float val)
        {
            if (_recieverDic.ContainsKey(uid))
            {
                _recieverDic[uid].TakeDamage(val);
            }
        }
    }
}
