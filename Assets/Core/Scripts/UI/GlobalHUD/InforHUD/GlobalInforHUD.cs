using Game.Scripts.BuildingLogic;
using Game.Scripts.StatsCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.HUD
{
    public class GlobalInforHUD : MonoBehaviour
    {
        [Header("Information")]
        [SerializeField] private Image avatar;
        [SerializeField] private TMP_Text nameTxt;
        [SerializeField] private TMP_Text healthTxt;
        [SerializeField] private TMP_Text armorTxt;
        [SerializeField] private TMP_Text levelTxt;
        [SerializeField] private TMP_Text damageTxt;

        public void ChangeHUD(CharacterBase character, Building building = null)
        {
            if (character == null && building == null) return;
            if (character != null)
            {
                character.Stats.OnHealthChange -= UpdateHealthText;
                character.Stats.OnHealthChange += UpdateHealthText;
                avatar.sprite = character.Stats.Avatar;
                nameTxt.text = character.Stats.Name;
                UpdateHealthText(character.Stats.Hp, character.Stats.MaxHp);
                armorTxt.text = character.Stats.Armor.ToString();
                levelTxt.text = character.Stats.Level.ToString();
                damageTxt.text = character.Stats.Damage.ToString();
                return;
            }

            building.Stats.OnHealthDetailChange -= UpdateHealthText;
            building.Stats.OnHealthDetailChange += UpdateHealthText;
            avatar.sprite = building.Stats.Model;
            nameTxt.text = building.Stats.BuildingName;
            UpdateHealthText(building.Stats.Health, building.Stats.MaxHp);
            armorTxt.text = building.Stats.Armor.ToString();
            levelTxt.text = building.Stats.Level.ToString();
            damageTxt.text = building.Stats.Damage.ToString();
            return;
        }

        private void UpdateHealthText(float hp, float maxHp)
        {
            healthTxt.text = $"{hp}/{maxHp}";
        }
    }
}

