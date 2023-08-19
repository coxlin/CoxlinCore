/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using UnityEngine;

namespace CoxlinCore.ObjectPool
{
    public abstract class PooledObject : MonoBehaviour
    {
        public bool IsActive;
        public virtual void OnAcquire()
        {
            IsActive = true;
        }
        public virtual void OnReturn()
        {
            IsActive = false;
        }
    }
}