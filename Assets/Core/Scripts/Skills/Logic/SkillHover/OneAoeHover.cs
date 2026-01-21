using Game.Scripts.BaseScripts;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    [CreateAssetMenu(fileName = "OneAoeHover", menuName = "Data/Skills/AoeHover/OneAoeHover")]
    public class OneAoeHover : SkillHover
    {
        public override void ShowHoverAoe(Vector3Int pos, int range)
        {
            trackPositions ??= new();
            trackPositions.Clear();
            targetPoint = pos;

            grid.ClearSkillHoverTile();
            grid.SetSkillHover(pos);
            trackPositions.Add(pos);
        }
    }
}


