using Game.Scripts.BaseScripts.Interface;
using Game.Scripts.BuildingLogic;
using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.StatsCharacter;
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

        protected SlotData data;
        protected IBaseGameObject baseGO;
        protected GlobalSlotHUD slotHUD;
        protected BuildManager buildManager => BuildManager.Instance;


        public void Init(GlobalSlotHUD globalSlot, IBaseGameObject baseGameObject, SlotData slotData)
        {
            data = slotData;
            baseGO = baseGameObject;
            iconSlot.sprite = slotData.Icon;
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
            OnUse();
            ShowSelectedBorder();
        }

        private void OnUse()
        {
            switch(data.Type)
            {
                case ESlot.Unit:
                    UnitSlotData unitSlotData = data as UnitSlotData;
                    StatsData statsData = unitSlotData.Data;
                    break;
                case ESlot.Building:
                    BuildingData buildingData = (data as BuildingSlotData).Data;
                    buildManager.SelectBuilding(buildingData.Type, buildingData.Key);
                    break;
                case ESlot.Upgrade:
                    (baseGO as Building).Upgrade();
                    break;
                case ESlot.Destroy:
                    (baseGO as Building).DestroyBuilding();
                    break;
            }
        }
    }
}

