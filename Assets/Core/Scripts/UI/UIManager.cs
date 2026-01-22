using Game.Scripts.BaseScripts.Interface;
using Game.Scripts.UI.HUD;
using SubScripts.Singleton;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UIManager : SingletonBase<UIManager>
    {
        [SerializeField] GlobalHUD globalHud;
        [SerializeField] ResourcesHUD resourcesHUD;
        public GlobalHUD HUD => globalHud;

        public void Init()
        {
            globalHud.Init();
            resourcesHUD.Init();
        }

        #region Global HUD
        public void HideHUD() => globalHud.gameObject.SetActive(false);
        public void ShowHUD() => globalHud.gameObject.SetActive(true);

        public void ChangeHUDOnSelect(IBaseGameObject baseGO)
        {
            globalHud.ChangeInforHUD(baseGO);
            globalHud.ChangeSlotHUD(baseGO);
        }

        #endregion
    }
}

