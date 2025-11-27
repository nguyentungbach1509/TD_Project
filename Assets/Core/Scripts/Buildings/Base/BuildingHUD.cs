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
            if (!progressBar.gameObject.activeSelf) progressBar.gameObject.SetActive(true);
            if (hpBar.gameObject.activeSelf) hpBar.gameObject.SetActive(false);
        }

        public void HideProgressBar()
        {
            progressBar.gameObject.SetActive(false);
            hpBar.gameObject.SetActive(true);
        }
    }
}

