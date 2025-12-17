using Game.Scripts.BuildingLogic;
using Subscripts.Spawn;
using System.Collections.Generic;
using System.Linq;
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

            for(int i = 0; i < BuildingController.GetAllData.Count; i++)
            {
                SlotHUD slot = spawner.SlotHUDSpawner.SpawnSlotHUD(slotContainer);
                slot.Init(this, BuildingController.GetDataAt(i));
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
    }

}
