using Game.Scripts.BaseScripts.Interface;
using UnityEngine;


namespace Game.Scripts.UI.HUD
{
    public class GlobalHUD : MonoBehaviour
    {
        [Header("Infor HUD")]
        [SerializeField] GlobalInforHUD inforHud;
        [Header("Slots HUD")]
        [SerializeField] private GlobalSlotHUD slotsHud;

        public void Init()
        {
            slotsHud.Init();
        }

        public void ChangeInforHUD(IBaseGameObject baseGO)
        {
            inforHud.ChangeHUD(baseGO);
        }

        public void ChangeSlotHUD(IBaseGameObject baseGO)
        {
            slotsHud.ChangeSlotHUD(baseGO);
        }
    }
}

