using Game.Scripts.BuildingLogic.Data;
using UnityEngine;

namespace Game.Scripts.UI.HUD
{
    [CreateAssetMenu(fileName = "BuildingSlotData", menuName ="Data/UI/SlotHUD/BuildingSlot")]
    public class BuildingSlotData : SlotData
    {
        [SerializeField] private BuildingData stats;

        public BuildingData Data => stats;
    }
}

