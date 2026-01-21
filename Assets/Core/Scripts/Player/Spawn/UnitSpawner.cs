using Game.Scripts.Map.Mechanic;
using Game.Scripts.ObstacleResource;
using Game.Scripts.Player;
using Game.Scripts.StatsCharacter;
using Subscripts;
using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Units
{
    public class UnitSpawner
    {
        private GamePrefabs gamePrefabs;
        private GridManager gridManager;
        private Dictionary<string, ObjectPool<CharacterBase>> poolUnit;
        
        public UnitSpawner(GamePrefabs gamePrefabs)
        {
            this.gamePrefabs = gamePrefabs;
            poolUnit = new();
            InitUnitPool();
        }

        private void InitUnitPool()
        {
            if (gamePrefabs == null)
            {
                Debug.LogError("GamePrefabSO không được gán trong Inspector!");
                return;
            }

            foreach (var data in gamePrefabs.UnitPrefabs.Prefabs)
            {
                poolUnit[data.Key] = PoolManager.CreateOrGetPool(data.Prefab, data.Key);
            }
        }

    }

}

