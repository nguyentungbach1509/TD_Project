using Game.Scripts.Manager;
using UnityEngine;
namespace Game.Scripts.UI
{
    public class FoodTabHUD : ResourcesTabHUD
    {
        public override void Init()
        {
            StorageController.OnFoodChange -= UpdateValue;
            StorageController.OnFoodChange += UpdateValue;
        }

        public override void UpdateValue(int value)
        {
            valueTxt.text = value.ToString();
        }
    }
}

