using Game.Scripts.BuildingLogic;
using Game.Scripts.StatsCharacter;
using UnityEngine;


namespace Game.Scripts.UI.HUD
{
    public class GlobalHUD : MonoBehaviour
    {
        [Header("Infor HUD")]
        [SerializeField] GlobalInforHUD inforHud;
        [Header("Slots HUD")]
        [SerializeField] private GlobalSlotHUD slots;

        public void Init()
        {
            slots.Init();
        }

        public void ChangeInforHUD(CharacterBase character, Building building = null)
        {
            inforHud.ChangeHUD(character, building);
        }
    }
}

