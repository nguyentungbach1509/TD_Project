using TMPro;
using UnityEngine;
namespace Game.Scripts.UI
{
    public abstract class ResourcesTabHUD : MonoBehaviour
    {
        [SerializeField] protected TMP_Text valueTxt;

        public abstract void Init();
        public abstract void UpdateValue(int value);
    }

}
