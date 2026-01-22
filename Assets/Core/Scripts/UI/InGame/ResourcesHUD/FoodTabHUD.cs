using Game.Scripts.Manager;
using UnityEngine;
namespace Game.Scripts.UI
{
    public class FoodTabHUD : ResourcesTabHUD
    {
        public override void Init()
        {
            CollectedResourcesController.OnFoodChange -= UpdateValue;
            CollectedResourcesController.OnFoodChange += UpdateValue;
        }

        public override void UpdateValue(int value)
        {
            valueTxt.text = value.ToString();
        }
    }
}

