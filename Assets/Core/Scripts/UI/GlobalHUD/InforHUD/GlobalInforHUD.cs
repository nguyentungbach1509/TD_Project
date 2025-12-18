using Game.Scripts.BaseScripts.Interface;
using Game.Scripts.BuildingLogic;
using Game.Scripts.StatsCharacter;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
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

        public void ChangeHUD(IBaseGameObject baseGO)
        {
            if (baseGO == null) return;
            baseGO.Stats.OnHealthDetailChange -= UpdateHealthText;
            baseGO.Stats.OnHealthDetailChange += UpdateHealthText;
            avatar.sprite = baseGO.Stats.Avatar;
            nameTxt.text = baseGO.Stats.Name;
            UpdateHealthText(baseGO.Stats.HP, baseGO.Stats.MaxHP);
            armorTxt.text = baseGO.Stats.Armor.ToString();
            levelTxt.text = baseGO.Stats.Level.ToString();
            damageTxt.text = baseGO.Stats.Damage.ToString();
            return;
        }

        private void UpdateHealthText(float hp, float maxHp)
        {
            healthTxt.text = $"{hp}/{maxHp}";
        }
    }
}

