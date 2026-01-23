using Game.Scripts.BaseScripts.Interface;
using Game.Scripts.BuildingLogic;
using Game.Scripts.BuildingLogic.Data;
using Subscripts.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.HUD
{
    public class GlobalSlotHUD : MonoBehaviour
    {
        [SerializeField] private List<SlotHUD> slots;
        [SerializeField] Transform slotContainer;

        private SpawnManager spawner => SpawnManager.Instance;
        private HashSet<string> saveHideSlots;

        public void Init()
        {
            slots ??= new();
            slots.Clear();
            saveHideSlots = new();

            Building.OnDestroy -= ShowAvailableSlot;
            Building.OnDestroy += ShowAvailableSlot;

            Building.OnBuild -= HideUnvailableSlot;
            Building.OnBuild += HideUnvailableSlot;
        }

        #region Updage Hud 
        public void ChangeSlotHUD(IBaseGameObject baseGO)
        {
            if (baseGO == null) return;
            
            List<SlotData> slotData = baseGO.Slots.List;
            
            SetupSlotHudHelper(slotData, baseGO);

            return;
        }

        private void SetupSlotHudHelper(List<SlotData> collection, IBaseGameObject baseGO)
        {
            ClearSlots();

            for (int i = 0; i < collection.Count; i++)
            {
                if (saveHideSlots.Contains(collection[i].Key)) continue;
                SlotHUD slot = spawner.SlotHUDSpawner.SpawnSlotHUD(slotContainer);
                slot.Init(this, baseGO, collection[i]);
                slots.Add(slot);
            }
        }

        public void HideOtherSlotBorder()
        {
            for(int i = 0; i < slots.Count; i++)
            {
                slots[i].HideSelectedBorder();
            }
        }

        public void ClearSlots()
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if (!slots[i].gameObject.activeSelf) saveHideSlots.Add(slots[i].Data.Key);
                spawner.SlotHUDSpawner.DespawnSlotHUD(slots[i]);
            }
            slots.Clear();

        }

        private void ShowAvailableSlot(Building building)
        {
            if(building.BuildingStats.Limit == EBuildLimit.One)
            {
                SlotHUD slotHud = slots.Find(s => s.Data.Key == building.Stats.Key);
                slotHud.gameObject.SetActive(true);
                if(saveHideSlots.Contains(building.Stats.Key)) saveHideSlots.Remove(building.Stats.Key);
            }
        }

        private void HideUnvailableSlot(Building building)
        {
            SlotHUD slotHud = slots.Find(s => s.Data.Key == building.Stats.Key);
            slotHud.gameObject.SetActive(false);
            saveHideSlots.Add(building.Stats.Key);
        }

        #endregion

        #region Editor Call
        [ContextMenu("Store Slot In List")]
        public void SetupSlot()
        {
            slots.Clear();
            slots.AddRange(GetComponentsInChildren<SlotHUD>(true));
        }
        #endregion
    }

}
