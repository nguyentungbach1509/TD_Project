using Game.Scripts.Manager;
using UnityEngine;
namespace Game.Scripts.UI
{
    public class GoldTabHUD : ResourcesTabHUD
    {
        public override void Init()
        {
            StorageController.OnGoldChange -= UpdateValue;
            StorageController.OnGoldChange += UpdateValue;
        }

        public override void UpdateValue(int value)
        {
            valueTxt.text = value.ToString();
        }
    }
}

