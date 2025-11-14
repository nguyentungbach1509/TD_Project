using UnityEngine;

namespace Game.Scripts.StatsCharacter.Canvas
{
    public class CharacterHUD : MonoBehaviour
    {
        [SerializeField] Healthbar healthbar;

        private Character stats;
        public Healthbar HpBar => healthbar;

        public void Init(Character character)
        {
            stats = character;
            character.OnTakeDamage += healthbar.UpdateHpBar;
        }
    }
}

