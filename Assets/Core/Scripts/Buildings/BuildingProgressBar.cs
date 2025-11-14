using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.BuidlingLogic.WorldUI
{
    public class BuildingProgressBar : MonoBehaviour
    {
        [SerializeField] Image fillImg;

        public void UpdateProgressBar(float percent)
        {
            fillImg.fillAmount = percent;
        }
    }
}

