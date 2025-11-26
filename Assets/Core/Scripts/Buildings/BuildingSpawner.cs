using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.Map.Mechanic;
using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuildingLogic
{
    public class BuildingSpawner
    {
        private GamePrefabs gamePrefabs;
        private GridManager gridManager;
        private Dictionary<string, ObjectPool<Building>> poolDict;

        public BuildingSpawner(GamePrefabs gamePrefabs)
        {
            this.gamePrefabs = gamePrefabs;
            gridManager = GridManager.Instance;
            poolDict = new();
        }

        private ObjectPool<Building> GetPool(EBuidlingType group, string key)
        {
            if (poolDict.TryGetValue(key, out var value)) return value;
            Building prefab = gamePrefabs.BuildingPrefabs.GetBuilding(group, key);
            ObjectPool<Building> pool = PoolManager.CreateOrGetPool(prefab, key);
            
            poolDict.Add(key, pool);
            return pool;
        }

        /// <summary>
        /// Spawn bang vi tri Vector3Int
        /// </summary>
        /// <param name="group"></param>
        /// <param name="key"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public Building SpawnBuilding(EBuidlingType group, string key, Vector3Int position, Quaternion rotation)
        {
            ObjectPool<Building> pool = GetPool(group, key);
            
            Vector3 worldPos = gridManager.GridToWorld(position);
            Building building = pool.Spawn(position, Quaternion.identity);
            building.Init();
            return building;
        }


        public void DespawnBuilding(Building building)
        {
            ObjectPool<Building> pool = poolDict[building.Stats.Key];
            pool.Despawn(building);
        }
    }
}

