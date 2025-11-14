using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace SubScripts.Pooling
{
    public class ObjectPool<T> where T : Component, IPoolable
    {
        private T prefab;
        private Queue<T> pool;
        private Transform parent;

        private int defaultCapacity;
        private bool autoExpand;
        private int maxSize;
        

        public ObjectPool(T prefab, int defaultCapacity = 10, bool autoExpand = true, int maxSize = 30)
        {
            //PoolKey = key;
            this.prefab = prefab;
            this.defaultCapacity = defaultCapacity;
            this.autoExpand = autoExpand;
            this.maxSize = maxSize;
            Initialize();
        }

        private void Initialize()
        {
            pool = new Queue<T>(defaultCapacity);
            parent = new GameObject($"Pool_{typeof(T).Name}").transform;

            for(int i = 0; i < defaultCapacity; i++)
            {
                CreateNewInstance();
            }
        }

        private T CreateNewInstance()
        {
            var instance = GameObject.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
            return instance;
        }

        public T Spawn(Vector3 position = default, Quaternion rotation = default)
        {
            if(pool.Count == 0 && autoExpand && pool.Count < maxSize)
            {
                int expandBy = Math.Min(5, maxSize - pool.Count); // Mở rộng thêm 5 thể hiện hoặc ít hơn nếu gần đạt maxSize
                for (int i = 0; i < expandBy; i++)
                {
                    CreateNewInstance();
                }
            }

            if(pool.Count == 0)
            {
                Debug.LogWarning($"Pool of {typeof(T).Name} is empty and cannot expand!");
                return null;
            }

            T instance = pool.Dequeue();
            instance.transform.SetPositionAndRotation(position, rotation);
            instance.gameObject.SetActive(true);
            instance.OnSpawn();
            return instance;
        }

        public void Despawn(T instance)
        {
            if (instance == null) return;

            instance.OnDespawn();
            instance.transform.parent = parent;
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }

        public async Task DespawnDelayed(T instance, float delay)
        {
            if (instance == null) return;

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(delay));

                if (instance != null)
                {
                    Despawn(instance);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error in DespawnDelayed: {e.Message}");
            }
        }

        
        public void DespawnDelayedNoWait(T instance, float delay)
        {
            _ = DespawnDelayed(instance, delay);
        }


        public void Clear()
        {
            while (pool.Count > 0)
            {
                var instance = pool.Dequeue();  
                if(instance != null)  GameObject.Destroy(instance.gameObject);  
            }
        }
    }
}

