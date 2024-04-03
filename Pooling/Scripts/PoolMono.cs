using System.Collections.Generic;
using UnityEngine;

namespace VFoundation.Pooling.Scripts
{
    public class PoolMono <T> where T: MonoBehaviour
    {
        private T Prefab { get; }
        public bool AutoExpand { get; set; }
        private Transform Container { get; }
        
        private List<T> _pool;

        public PoolMono(T prefab,int count)
        {
            Prefab = prefab;
            Container = null;

            CreatePool(count);
        }

        public PoolMono(T prefab,int count,Transform container)
        {
            Prefab = prefab;
            Container = container;

            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();

            for (int i  = 0;i < count; i++) 
            {
                CreateObject();
            }
        }

        private T CreateObject(bool activeByDefault = false)
        {
            var createdObject = Object.Instantiate(Prefab,Container);
            createdObject.gameObject.SetActive(activeByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        public bool HasFreeElement(out T element)
        {
            foreach (var mono in _pool)
            {
                if (mono.gameObject.activeSelf)
                    continue;

                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;

            if (AutoExpand)
                return CreateObject();

            throw new System.Exception($"Ther is no free element in pool of type {typeof(T)}");
        }
    }
}