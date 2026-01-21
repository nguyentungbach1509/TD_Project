using Game.Scripts.BuildingLogic;
using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.ObstacleResource;
using Game.Scripts.UICustom;
using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class ResourceTxtSpawner
    {
        private UIPrefabs uiPrefabs;
        private GridManager gridManager;
        private Dictionary<string, ObjectPool<TMP_Text_Custom>> poolText;

        public ResourceTxtSpawner(UIPrefabs uiPrefabs) 
        {
            this.uiPrefabs = uiPrefabs;
            gridManager = GridManager.Instance;
            poolText = new();
            InitTextPool();
        }

        private void InitTextPool()
        {
            if (uiPrefabs == null)
            {
                Debug.LogError("UIPrefabSO không được gán trong Inspector!");
                return;
            }

            foreach (var data in uiPrefabs.ResourceTxts.Prefabs)
            {
                poolText[data.Key] = PoolManager.CreateOrGetPool(data.Prefab, data.Key);
            }
        }

        public TMP_Text_Custom SpawnCustomText(string key, Vector3 position)
        {
            if (!poolText.ContainsKey(key))
            {
                Debug.LogError($"Text với key {key} không tồn tại trong pool!");
                return null;
            }

            ObjectPool<TMP_Text_Custom> pool = poolText[key];
            TMP_Text_Custom text = pool.Spawn(position, Quaternion.identity);
            return text;
        }

        public void DespawnCustomText(string key, TMP_Text_Custom text)
        {
            ObjectPool<TMP_Text_Custom> pool = poolText[key];
            pool.Despawn(text);
        }
    }
}

