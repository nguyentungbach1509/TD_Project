using Game.Scripts.BaseScripts;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    [CreateAssetMenu(fileName = "SquareAoeHover", menuName = "Data/Skills/AoeHover/SquareAoeHover")]
    public class SquareAoeHover : SkillHover
    {
        public override void ShowHoverAoe(Vector3Int pos, int range)
        {
            trackPositions ??= new();
            trackPositions.Clear();
            targetPoint = pos;

            grid.ClearSkillHoverTile();

            int x = -range / 2;
            
            for(int i = 0; i < range; i++)
            {
                int y = -range / 2;
                for (int j = 0; j < range; j++)
                {
                    Vector3Int position = pos + new Vector3Int(x, y);
                    grid.SetSkillHover(position);
                    trackPositions.Add(position);
                    y++;
                }
                x++;
            }
        }
    }
}

