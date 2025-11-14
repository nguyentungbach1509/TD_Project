using Game.Scripts.StatsCharacter.WorldUI;
using UnityEngine;

namespace Game.Scripts.BuidlingLogic.WorldUI
{
    public class BuildingHUD
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
    }
}

