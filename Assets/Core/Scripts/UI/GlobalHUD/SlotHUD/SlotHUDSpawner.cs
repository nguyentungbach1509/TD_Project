using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.HUD
{
    public class SlotHUDSpawner
    {
        private UIPrefabs uiPrefabs;
        private ObjectPool<SlotHUD> slotPool;


        public SlotHUDSpawner(UIPrefabs uIPrefabs)
        {
            this.uiPrefabs = uIPrefabs;
            slotPool = PoolManager.CreateOrGetPool(uiPrefabs.SlotPrefab.Prefab);
        }

        public SlotHUD SpawnSlotHUD(Transform parent)
        {
            SlotHUD slot = slotPool.Spawn();
            slot.transform.SetParent(parent, false);
            slot.transform.localPosition = Vector3.zero;
            slot.transform.localScale = Vector3.one;
            return slot;
        }

        public void DespawnSlotHUD(SlotHUD slot)
        {
            slotPool.Despawn(slot);
        }
    }
}

