using Game.Scripts.Manager;
using UnityEngine;
namespace Game.Scripts.UI
{
    public class LumberTabHUD : ResourcesTabHUD
    {
        public override void Init()
        {
            CollectedResourcesController.OnLumberChange -= UpdateValue;
            CollectedResourcesController.OnLumberChange += UpdateValue;
        }

        public override void UpdateValue(int value)
        {
            valueTxt.text = value.ToString();
        }
    }
}

