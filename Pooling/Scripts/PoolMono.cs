using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Pooling
{
    public class PoolMono <T> where T : Component
    {
        public T Prefab { get; private set; }
        private T[] Prefabs { get; }
        public bool AutoExpand { get; set; }
        private Transform Container { get; }
        
        private List<T> _pool;
        private bool _random;

        public PoolMono(T prefab,int count)
        {
            Prefab = prefab;
            Container = new GameObject(prefab.name + " Container").transform;
            Object.DontDestroyOnLoad(Container);
            CreatePool(count);
        }

        public PoolMono(T prefab,int count,Transform container)
        {
            Prefab = prefab;
            Container = container;

            CreatePool(count);
        }
        
        public PoolMono(T[] prefabs,int count,bool random)
        {
            Prefabs = prefabs;
            Prefab = Prefabs.TakeRandom();
            _random = random;

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
            if(_random)
                Prefab = Prefabs.TakeRandom();
            return createdObject;
        }

        private bool HasFreeElement(bool enable,out T element)
        {
            foreach (var mono in _pool)
            {
                if (mono.gameObject.activeSelf)
                    continue;

                element = mono;
                mono.gameObject.SetActive(enable);
                return true;
            }

            element = null;
            return false;
        }

        private bool HasFreeElement(out T element)
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

        public bool HasFreeElement()
        {
            foreach (var mono in _pool)
            {
                if (mono.gameObject.activeSelf)
                    continue;
                return true;
            }
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out var element))
                return element;

            if (AutoExpand)
                return CreateObject();

            throw new System.Exception($"There is no free element in pool of type {typeof(T)}");
        }
        
        public T GetFreeElementDisabled()
        {
            if (HasFreeElement(false, out var element))
                return element;

            if (AutoExpand)
                return CreateObject();

            throw new System.Exception($"There is no free element in pool of type {typeof(T)}");
        }

        public List<T> AllElements() => _pool;
    }
}