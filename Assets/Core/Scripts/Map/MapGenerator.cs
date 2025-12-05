using Game.Scripts.Map.Hills;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.Map.Mechanic
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Map Configs")]
        [SerializeField] int width;
        [SerializeField] int height;

        [Header("Hill Settings")]
        [SerializeField] Tilemap hillTileMap;
        [SerializeField] HillDataCollection data;
        [SerializeField] float percentHill;

        [Header("Respawn Area")]
        [SerializeField] int areaWidth;
        [SerializeField] int areaHeight;

        private List<Vector3Int> spawnBorderList;

        private GridManager gridManager => GridManager.Instance;


        public void Init()
        {
            spawnBorderList = new List<Vector3Int>();
            BuildMap();
        }


        private void BuildMap()
        {
            int x = -width / 2;
            for (int i = 0; i < width; i++)
            {
                int y = -height / 2;
                for (int j = 0; j < height; j++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    gridManager.SetGround(position);
                    if(i == 0 || j == 0 || i == width-1 ||  j == height-1) spawnBorderList.Add(position);
                    else BuildRespawnArea(new Vector3Int(x, y, 0));
                    y++;
                }

                x++;
            }


            HashSet<Hill> hills = new();
			List<Vector3Int> candidatePositions = new List<Vector3Int>(gridManager.GroundDict.Keys);

            foreach (Vector3Int pos in candidatePositions)
            {
                if (spawnBorderList.Contains(pos)) continue;
                if (!ValidateHillPosition(hills, pos)) continue;
                if(Random.value < percentHill)
                {
                    Hill hill = new Hill(data, hillTileMap);
                    hill.BuildHill(pos, spawnBorderList);
                    if (hill.Positions.Count > 0) hills.Add(hill);
                }
            }

        }

        private void BuildRespawnArea(Vector3Int position)
        {
            if (position != Vector3Int.zero) return;
            int x = -(areaWidth+1) / 2;
            for(int i = 0; i <= areaWidth; i++)
            {
                int y = -(areaHeight+1) / 2;
                for(int j = 0; j <= areaHeight; j++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    if (i == 0 || j == 0 || i == areaWidth || j == areaHeight)
                    {
                        gridManager.SetRespawnArea(pos, true);
                    }
                    else gridManager.SetRespawnArea(pos);
                    y++;
                }
                x++;
            }
        }
		

        private bool ValidateHillPosition(HashSet<Hill> hills, Vector3Int pos)
        {
            if (hills.Count == 0) return true;
            foreach(var hill in hills)
            {

                if (Mathf.Abs(pos.x - hill.Positions[0].x) <= hill.Width &&
                           Mathf.Abs(pos.y - hill.Positions[0].y) <= hill.Height) return false;
            }
            
            return true;
        }

        private void SetTreePlace()
        {
            for(int i = 0; i < gridManager.GroundDict.Count; i++)
            {

            }
        }
    }

}
