using UnityEngine;

namespace Game.Scripts.StatsCharacter.WorldUI
{
    public class CharacterHUD : MonoBehaviour
    {
        [SerializeField] Healthbar healthbar;
        [SerializeField] GameObject selectedDetection;
        [SerializeField] Canvas canvas;

        private Character stats;
        public Healthbar HpBar => healthbar;

        public void Init()
        {
            canvas.worldCamera = Camera.main;
        }

        public void ShowSelectedDetection() => selectedDetection.SetActive(true);
        public void HideSelectedDetection() => selectedDetection.SetActive(false);
    }
}

