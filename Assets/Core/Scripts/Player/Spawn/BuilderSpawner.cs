using Game.Scripts.GamePlay;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.ObstacleResource;
using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class BuilderSpawner
    {
        private GamePrefabs gamePrefabs;
        private Dictionary<string, ObjectPool<BuilderController>> poolBuilder;

        public BuilderSpawner(GamePrefabs gamePrefabs)
        {

            this.gamePrefabs = gamePrefabs;
            poolBuilder = new();

            InitBuilderPool();
        }

        public void InitBuilderPool()
        {
            if (gamePrefabs == null)
            {
                Debug.LogError("GamePrefabSO không được gán trong Inspector!");
                return;
            }

            foreach (var data in gamePrefabs.BuilderPrefabs.Prefabs)
            {
                poolBuilder[data.Key] = PoolManager.CreateOrGetPool(data.Prefab as BuilderController, data.Key);
            }
        }

        public BuilderController SpawnBuilder(string key, Vector3Int position, Quaternion rotation)
        {           
            if (!poolBuilder.ContainsKey(key))
            {
                Debug.LogError($"Builder với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<BuilderController> pool = poolBuilder[key];
            BuilderController builder = pool.Spawn(position, rotation);
            builder.Init();
            UnitController.AddCharacter(position, builder);
            return builder;
        }

        public void DespawnBuilder(string key, BuilderController builder)
        {
            ObjectPool<BuilderController> pool = poolBuilder[key];
            pool.Despawn(builder);
        }
    }
}

