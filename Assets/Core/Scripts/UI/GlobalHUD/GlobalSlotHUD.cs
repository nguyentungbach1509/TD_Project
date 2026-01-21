using Game.Scripts.BaseScripts.Interface;
using Subscripts.Spawn;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.HUD
{
    public class GlobalSlotHUD : MonoBehaviour
    {
        [SerializeField] private List<SlotHUD> slots;
        [SerializeField] Transform slotContainer;

        private SpawnManager spawner => SpawnManager.Instance;

        public void Init()
        {
            slots ??= new();
            slots.Clear();
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
                spawner.SlotHUDSpawner.DespawnSlotHUD(slots[i]);
            }
            slots.Clear();

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
