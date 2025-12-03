using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.BuildingLogic.WorldUI
{
    public class BuildingProgressBar : MonoBehaviour
    {
        [SerializeField] Image fillImg;


        public void UpdateProgressBar(float percent)
        {
            fillImg.fillAmount = percent;
        }

        public void ResetAmount() => fillImg.fillAmount = 0;
    }
}

