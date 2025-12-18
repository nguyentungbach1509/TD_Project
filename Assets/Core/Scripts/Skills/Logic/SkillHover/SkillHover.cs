using Game.Scripts.Map.Mechanic;
using UnityEngine;

namespace Game.Scripts.BaseScripts
{
    public abstract class SkillHover : ScriptableObject
    {
        protected GridManager grid => GridManager.Instance;
        public abstract void ShowHoverAoe(Vector3Int pos, int range);   
        
    }
}
