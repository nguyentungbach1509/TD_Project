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
        private Dictionary<string, ObjectPool<Building>> poolBuilding;
        private Dictionary<EBuildingType, List<ObjectPool<BuildingModel>>> poolModel; 

        public BuildingSpawner(GamePrefabs gamePrefabs)
        {
            this.gamePrefabs = gamePrefabs;
            gridManager = GridManager.Instance;
            poolBuilding = new();
        }

        #region Building

        private ObjectPool<Building> GetPool(EBuildingType group, string key)
        {
            if (poolBuilding.TryGetValue(key, out var value)) return value;
            Building prefab = gamePrefabs.BuildingPrefabs.GetBuilding(group, key);
            ObjectPool<Building> pool = PoolManager.CreateOrGetPool(prefab, key);
            
            poolBuilding.Add(key, pool);
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
        public Building SpawnBuilding(EBuildingType group, string key, Vector3Int position, Quaternion rotation)
        {
            ObjectPool<Building> pool = GetPool(group, key);
            ObjectPool<BuildingModel> modelPool = GetModelPool(group, key, 1);
            
            Vector3 worldPos = gridManager.GridToWorld(position);
            Building building = pool.Spawn(position, Quaternion.identity);
            BuildingModel model = modelPool.Spawn();
            BuildingData data = gamePrefabs.BuildingPrefabs.GetData(group, key);
            
            model.transform.SetParent(building.transform, false);
            model.transform.localPosition = Vector3.zero;   
            
            building.SetupModel(model);
            building.Init(data);
            return building;
        }


        public void DespawnBuilding(Building building)
        {
            ObjectPool<Building> pool = poolBuilding[building.Stats.Key];
            ObjectPool<BuildingModel> modelPool = GetModelPool(building.Stats.Type, building.Stats.Key, building.Stats.Level);
            modelPool.Despawn(building.Model);
            pool.Despawn(building);
        }
        #endregion

        #region Model
        private ObjectPool<BuildingModel> GetModelPool(EBuildingType group, string key, int level)
        {
            if (!poolModel.TryGetValue(group, out var pools) || pools == null)
            {
                pools = new List<ObjectPool<BuildingModel>>();
                poolModel[group] = pools;
                UpdateRequirement[] requirements = gamePrefabs.BuildingPrefabs.GetData(group, key).Requirements;
                BuildingModel model = requirements[level - 1].UpdateModel.Prefab;
                string modelKey = $"{key}_{level}";
                ObjectPool<BuildingModel> pool = PoolManager.CreateOrGetPool(model, modelKey);
                pools.Add(pool);
                return pool;
            }

            return pools[level - 1];
        } 

        public BuildingModel SpawnModel(EBuildingType group, string key, int level)
        {
            ObjectPool<BuildingModel> modelPool = GetModelPool(group, key, level-1);
            BuildingModel model = modelPool.Spawn();
            return model;
        }

        public void DespawnModel(Building building)
        {
            ObjectPool<BuildingModel> modelPool = GetModelPool(building.Stats.Type, building.Stats.Key, building.Stats.Level);
            modelPool.Despawn(building.Model);
        }
        #endregion
    }
}

