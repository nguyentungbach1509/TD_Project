
using Game.Scripts.Map.Mechanic;
using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ObstacleResource
{
    public class TreeSpawner
    {
        private GamePrefabs gamePrefabs;
        private GridManager gridManager;
        private Dictionary<string, ObjectPool<TreeSource>> poolTree;
        private Dictionary<string, float> weights;
        private bool isInitialized = false;

        public bool IsInitialized() => isInitialized;

        public TreeSpawner(GamePrefabs gamePrefabs)
        {
            if (isInitialized) return;

            this.gamePrefabs = gamePrefabs;
            gridManager = GridManager.Instance;
            weights = new Dictionary<string, float>();
            poolTree = new();

            InitTreePool();
            isInitialized = true;
        }

        private void InitTreePool()
        {
            if(gamePrefabs == null)
            {
                Debug.LogError("GamePrefabSO không được gán trong Inspector!");
                return;
            }

            foreach (var data in gamePrefabs.TreePrefabs.Prefabs)
            {
                poolTree[data.Key] = PoolManager.CreateOrGetPool(data.Prefab, data.Key);
                weights.Add(data.Key, data.RandomPercent);
            }
        }

        private string GetRandomTreeKey()
        {
            float totalPercent = 0;
            
            foreach(var weight in weights)
            {
                totalPercent += weight.Value;
            }

            float random = Random.Range(0, totalPercent);

            // Tìm item tương ứng
            float cumulative = 0f;
            string key = string.Empty;
            foreach (var element in weights)
            {
                cumulative += element.Value;
                key = element.Key;
                if (random <= cumulative)
                    return element.Key;
            }

            return key;
        }

        public TreeSource SpawnTree(Vector3 position, Quaternion rotation)
        {
            string key = GetRandomTreeKey();

            if (!poolTree.ContainsKey(key))
            {
                Debug.LogError($"Tree với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<TreeSource> pool = poolTree[key];
            TreeSource tree = pool.Spawn(position, rotation);
            tree.Init(key);
            return tree;
        }

        public void DespawnTree(TreeSource tree)
        {
            ObjectPool<TreeSource> pool = poolTree[tree.Key];
            pool.Despawn(tree);
        } 
    }
}

