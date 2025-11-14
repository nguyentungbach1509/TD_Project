using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.StatsCharacter.WorldUI
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] Image fillImg;

        public void UpdateHpBar(float percent)
        {
            fillImg.fillAmount = percent;
        }
    }
}

