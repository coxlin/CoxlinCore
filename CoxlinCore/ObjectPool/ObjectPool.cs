/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace CoxlinCore.ObjectPool
{
    public sealed class ObjectPool : MonoBehaviour
    {
        [SerializeField] private PooledObjectData[] _pooledObjects;
        [SerializeField] private Transform _parent;
        private readonly HashSet<PooledObject> _objectsInUse = new HashSet<PooledObject>();
        private Dictionary<string, PooledObject[]> _pooledObjectDic = new Dictionary<string, PooledObject[]>();

        public void CreateObjectsFromPool(Action? onCreateAction = null)
        {
            StartCoroutine(CreateObjectsCoroutine());
        }

        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        private IEnumerator CreateObjectsCoroutine(
            Action? onCreateAction = null)
        {
            int count = _pooledObjects.Length;
            _pooledObjectDic.Clear();

            for (int i = 0; i < count; ++i)
            {
                onCreateAction?.Invoke();
                var p = _pooledObjects[i];
                var pooledObjs = new PooledObject[p.Count];
                _pooledObjectDic[p.Name] = pooledObjs;
                int pooledObjsLength = pooledObjs.Length;

                for (int j = 0; j < p.Count; ++j)
                {
                    PooledObject pooledObject;
                    if (j < pooledObjsLength)
                    {
                        pooledObject = pooledObjs[j];
                    }
                    else
                    {
                        pooledObject = Instantiate(p.PooledObject, _parent);
                        pooledObject.transform.localPosition = Vector2.zero;
                    }
                    pooledObject.gameObject.SetActive(false);
                    pooledObject.OnReturn(); // Prepare the object for reuse
                }

                yield return null;
            }
        }


        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        public PooledObject Acquire(
            string objName,
            Vector3 position,
            bool activateOnAcquire = true)
        {
            PooledObject pooledObject = null;
            if (!_pooledObjectDic.ContainsKey(objName))
            {
                throw new UnityException($"{objName} is not in the object pool dic");
            }

            var pooledObjs = _pooledObjectDic[objName];
            if (_objectsInUse.ContainsAll(pooledObjs))
            {
                throw new UnityException($"All objects for {objName} are already in use");
            }

            int count = pooledObjs.Length;
            for (int i = 0; i < count; ++i)
            {
                if (_objectsInUse.Contains(pooledObjs[i])) continue;
                    
                pooledObject = pooledObjs[i];
                _objectsInUse.Add(pooledObject);
                pooledObject.transform.position = position;
                pooledObject.gameObject.SetActive(activateOnAcquire);
                pooledObject.OnAcquire();
                break;
            }

            if (pooledObject == null)
            {
                throw new UnityException($"Trying to grab a null object for {objName}");
            }
            return pooledObject;
        }
        
        public void Return(
            PooledObject pooledObj)
        {
            _objectsInUse.Remove(pooledObj);
            if (pooledObj == null)
            {
                return;
            }
            pooledObj.OnReturn();

            pooledObj.transform.position = _parent.position;
            pooledObj.gameObject.SetActive(false);
        }
    }
}