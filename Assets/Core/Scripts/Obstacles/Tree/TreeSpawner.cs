
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
        private Dictionary<string, ObjectPool<Tree>> poolTree;
        private bool isInitialized = false;

        public bool IsInitialized() => isInitialized;

        public TreeSpawner(GamePrefabs gamePrefabs)
        {
            if (isInitialized) return;

            this.gamePrefabs = gamePrefabs;
            gridManager = GridManager.Instance;
            poolTree = new();

            InitTreePool();
            isInitialized = true;
        }

        public void InitTreePool()
        {
            if(gamePrefabs == null)
            {
                Debug.LogError("GamePrefabSO không được gán trong Inspector!");
                return;
            }

            foreach (var data in gamePrefabs.TreePrefabs.Prefabs)
            {
                poolTree[data.Key] = PoolManager.CreateOrGetPool(data.Prefab, data.Key);
            }
        }

        public Tree SpawnTree(string key, Vector3Int position, Quaternion rotation)
        {
            if (!poolTree.ContainsKey(key))
            {
                Debug.LogError($"Tree với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<Tree> pool = poolTree[key];
            Tree tree = pool.Spawn(position, rotation);
            tree.Init(key);
            return tree;
        }

        public void DespawnTree(Tree tree)
        {
            ObjectPool<Tree> pool = poolTree[tree.Key];
            pool.Despawn(tree);
        } 
    }
}

