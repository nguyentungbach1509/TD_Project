using SubScripts.Pooling;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Projectiles
{
    public class ProjectileSpawner
    {
        private GamePrefabs gamePrefabs;
        private Dictionary<string, ObjectPool<Projectile>> poolProjectile;

        public ProjectileSpawner(GamePrefabs gamePrefabs)
        {
            this.gamePrefabs = gamePrefabs;
            poolProjectile = new();
            InitProjectilePool();
        }

        private void InitProjectilePool()
        {
            if (gamePrefabs == null)
            {
                Debug.LogError("GamePrefabSO không được gán trong Inspector!");
                return;
            }

            foreach (var data in gamePrefabs.ProjectilePrefabs.Prefabs)
            {
                poolProjectile[data.Key] = PoolManager.CreateOrGetPool(data.Prefab, data.Key);
            }
        }

        public Projectile SpawnProjectile(string key, Vector3 position, Quaternion rotation)
        {
            if (!poolProjectile.ContainsKey(key))
            {
                Debug.LogError($"Tree với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<Projectile> pool = poolProjectile[key];
            Projectile projectile = pool.Spawn(position, rotation);
            projectile.Init(key, DespawnTree);
            return projectile;
        }

        public void DespawnTree(Projectile projectile)
        {
            ObjectPool<Projectile> pool = poolProjectile[projectile.Key];
            pool.Despawn(projectile);
        }
    }
}


