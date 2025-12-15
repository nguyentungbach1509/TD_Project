using Game.Scripts.Map.Mechanic;
using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ObstacleResource
{
    public class OreSpawner
    {
        private GamePrefabs gamePrefabs;
        private Dictionary<string, ObjectPool<Ore>> poolOre;

        public OreSpawner(GamePrefabs gamePrefabs)
        {
            this.gamePrefabs = gamePrefabs;
            poolOre = new();

            InitOrePool();
        }

        public void InitOrePool()
        {
            if (gamePrefabs == null)
            {
                Debug.LogError("GamePrefabSO không được gán trong Inspector!");
                return;
            }

            foreach (var data in gamePrefabs.OrePrefabs.Prefabs)
            {
                poolOre[data.Key] = PoolManager.CreateOrGetPool(data.Prefab, data.Key);
            }
        }

        public Ore SpawnOre(string key, Vector3 position)
        {
            if (!poolOre.ContainsKey(key))
            {
                Debug.LogError($"Ore với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<Ore> pool = poolOre[key];
            Ore ore = pool.Spawn(position, Quaternion.identity);
            ore.Init(key);
            return ore;
        }

        public void DespawnOre(Ore ore)
        {
            ObjectPool<Ore> pool = poolOre[ore.Key];
            pool.Despawn(ore);
        }
    }
}

