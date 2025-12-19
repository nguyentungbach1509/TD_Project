using Game.Scripts.Map.Mechanic;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BaseScripts
{
    public abstract class SkillHover : ScriptableObject
    {
        protected GridManager grid => GridManager.Instance;
        protected HashSet<Vector3Int> trackPositions;

        public HashSet<Vector3Int> TrackPos => trackPositions;
        public abstract void ShowHoverAoe(Vector3Int pos, int range);   
        public void ClearHoverAoe()
        {
            grid.ClearSkillHoverTile();
        }
    }
}
