using Game.Scripts.BaseScripts;
using UnityEngine;
namespace Game.Scripts.SkillMechanic
{
    [CreateAssetMenu(fileName = "CrossAoeHover", menuName = "Data/Skills/AoeHover/CrossAoeHover")]
    public class CrossAoeHover : SkillHover
    {
        public override void ShowHoverAoe(Vector3Int pos, int range)
        {
            trackPositions ??= new();
            trackPositions.Clear();

            grid.ClearSkillHoverTile();
            int x = -range / 2;
            int y = -range / 2;

            for(int i = 0; i < range; i++)
            {
                Vector3Int position = pos + new Vector3Int(x, 0, 0);
                grid.SetSkillHover(position);
                trackPositions.Add(position);
                x++;
            }

            for(int j = 0; j < range; j++)
            {
                if (y == 0) continue;
                Vector3Int position = pos + new Vector3Int(0, y, 0);
                grid.SetSkillHover(position);
                trackPositions.Add(position);
                y++;
            }
        }
    }
}

