using Game.Scripts.StatsCharacter;
using UnityEngine;
namespace Game.Scripts.UI.HUD
{
    [CreateAssetMenu(fileName = "UnitSlotData", menuName = "Data/UI/SlotHUD/UnitSlot")]
    public class UnitSlotData : SlotData
    {
        [SerializeField] private StatsData stats;

        public StatsData Data => stats;
    }

}
