using Game.Scripts.BuildingLogic;
using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.UI.HUD;
using SubScripts.Pooling;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    
    public class SlotHUD : PoolableComponent, IPointerClickHandler
    {
        [SerializeField] protected Image iconSlot;
        [SerializeField] protected GameObject selectedBorder;

        private BuildingData building;
        
        protected GlobalSlotHUD slotHUD;
        private BuildManager buildManager => BuildManager.Instance;

        public void Init(GlobalSlotHUD globalSlot, BuildingData building)
        {
            this.building = building;
            iconSlot.sprite = building.Icon;
            slotHUD = globalSlot;
        }

        public void ShowSelectedBorder() => selectedBorder.SetActive(true);
        public void HideSelectedBorder() => selectedBorder.SetActive(false);

        public override void OnSpawn() => HideSelectedBorder();
        
        public override void OnDespawn()
        {
            
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            slotHUD.HideOtherSlotBorder();
            buildManager.SelectBuilding(building.Type, building.Key);
            ShowSelectedBorder();
        }
    }
}

