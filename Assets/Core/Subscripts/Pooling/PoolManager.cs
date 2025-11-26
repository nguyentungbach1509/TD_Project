using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Huong dan su dung - Vi du cho pool projectile
 * public class ProjectileManager : MonoBehaviour {
 *  [SerializeField] private Projectile projectilePrefab;
 *  private ObjectPool<Projectile> projectilePool;
 *  
 *  private void Start(){
 *      //Khoi tao pool
 *      projectilePool = PoolManager.CreateOrGetPool(projectilePrefab, 20);
 *  }
 *  
 *  public FireProjectile(Vector3 position, Quaternion rotation) {
 *      var projectile = projectilePool.Spawn(position, rotation);
 *      
 *      //Despawn ngay lap tuc
 *      projectilePool.Depsawn(projectile);
 *      
 *      //Tu dong despawn sau 3 giay
 *      projectilePool.DepsawnDelayed(projectile,3f);
 *  
 *  }
 *  
 *  }
 */

namespace SubScripts.Pooling
{
    public static class PoolManager
    {
        private static Dictionary<Type, object> pools = new Dictionary<Type, object>();
        private static Dictionary<string, ObjectPool<PoolableComponent>> customPools = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            ClearAllPool();
            ClearAllKeyPool();
            pools = new Dictionary<Type, object>();
            customPools = new();
        }

        public static ObjectPool<T> CreateOrGetPool<T>(T prefab, int defaultCapacity = 10, bool autoExpand = true, int maxSize = 50) where T : PoolableComponent
        {
            var type = typeof(T);
            if (pools.ContainsKey(type))
            {
                Debug.LogWarning($"Pool for {type.Name} already exists!");
                return GetPool<T>();
            }

            var pool = new ObjectPool<T>(prefab, defaultCapacity, autoExpand, maxSize);
            pools.Add(type, pool);
            return pool;
        }

        public static ObjectPool<T> CreateOrGetPool<T>(T prefab, string key, int defaultCapacity = 10, bool autoExpand = true, int maxSize = 50) where T: PoolableComponent  
        {
            if (customPools.ContainsKey(key))
            {
                Debug.LogWarning($"Pool for key {key} already exists!");
                return customPools[key] as ObjectPool<T>;
            }

            var pool = new ObjectPool<T>(prefab, defaultCapacity, autoExpand, maxSize);
            customPools.Add(key, pool as ObjectPool<PoolableComponent>);
            return pool;
        }

        private static ObjectPool<T> GetPool<T>() where T : PoolableComponent
        {
            var type = typeof(T);
            if (pools.TryGetValue(type, out object pool))
            {
                return (ObjectPool<T>)pool;
            }

            return null;
        }

        public static void ClearPool<T>() where T : PoolableComponent
        {
            var type = typeof(T);
            if (pools.TryGetValue(@type, out object pool))
            {
                ((ObjectPool<T>)pool).Clear();
                pools.Remove(type);
            }
        }

        // Phương thức xóa pool dựa trên key
        public static void ClearPool(string key)
        {
            if (customPools.TryGetValue(key, out ObjectPool<PoolableComponent> pool))
            {
                pool.Clear();
                customPools.Remove(key);
            }
        }

        public static void ClearAllPool()
        {
            foreach (var pool in pools.Values)
            {
                var methodInfo = pool.GetType().GetMethod("Clear");
                methodInfo.Invoke(pool, null);
            }
            pools.Clear();
        }

        // Phương thức xóa tất cả các pool
        public static void ClearAllKeyPool()
        {
            foreach (var pool in customPools.Values)
            {
                pool.Clear();
            }
            customPools.Clear();
        }
    }

}

