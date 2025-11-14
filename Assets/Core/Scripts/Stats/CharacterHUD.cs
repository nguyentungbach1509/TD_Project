using UnityEngine;

namespace Game.Scripts.StatsCharacter.WorldUI
{
    public class CharacterHUD : MonoBehaviour
    {
        [SerializeField] Healthbar healthbar;
        [SerializeField] Canvas canvas;

        private Character stats;
        public Healthbar HpBar => healthbar;

        public void Init()
        {
            canvas.worldCamera = Camera.main;
        }
    }
}

