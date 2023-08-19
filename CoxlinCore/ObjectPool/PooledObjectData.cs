/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

namespace CoxlinCore.ObjectPool
{
    [System.Serializable]
    public class PooledObjectData
    {
        public string Name;
        public PooledObject PooledObject;
        public int Count;
    }
}