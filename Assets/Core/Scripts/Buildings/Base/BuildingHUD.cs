using Game.Scripts.StatsCharacter.WorldUI;
using UnityEngine;

namespace Game.Scripts.BuildingLogic.WorldUI
{
    public class BuildingHUD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] Canvas canvas;
        [SerializeField] private Healthbar hpBar;
        [SerializeField] private BuildingProgressBar progressBar;

        public Healthbar HpBar => hpBar;
        public BuildingProgressBar ProgressBar => progressBar;

        public void Init()
        {
            canvas.worldCamera = Camera.main;
        }

        public void HideProgressBar() => progressBar.gameObject.SetActive(false);
        public void ShowProgressBar()
        {
            progressBar.gameObject.SetActive(true);
            progressBar.ResetAmount();
        }

        public void HideHealthBar() => hpBar.gameObject.SetActive(false);
        public void ShowHealthBar() => hpBar.gameObject.SetActive(true);
    }
}

