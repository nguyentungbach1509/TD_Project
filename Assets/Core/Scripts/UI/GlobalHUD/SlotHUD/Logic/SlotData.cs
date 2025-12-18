using UnityEngine;

namespace Game.Scripts.UI.HUD
{
    public enum ESlot
    {
        None,
        Building,
        Unit,
        Skill,
        Upgrade,
        Destroy
    }

    public abstract class SlotData : ScriptableObject
    {
        [SerializeField] protected ESlot type;
        [SerializeField] protected string key;
        [SerializeField] protected Sprite icon;

        public ESlot Type => type;
        public string Key => key;
        public Sprite Icon => icon;

    }
}

