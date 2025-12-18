using Game.Scripts.BaseScripts.Abstract;
using Game.Scripts.UI.HUD;


namespace Game.Scripts.BaseScripts.Interface
{
    public interface IBaseGameObject
    {
        public Stats Stats { get; }
        public SlotCollection Slots { get; }
    }

}

