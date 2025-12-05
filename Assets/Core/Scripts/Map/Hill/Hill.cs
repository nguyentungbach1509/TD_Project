using Game.Scripts.Map.Mechanic;
using Game.Scripts.ObstacleResource;
using Game.Scripts.TileController.Mechanic;
using Subscripts.Spawn;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.Map.Hills
{
    public class Hill
    {
        private List<Vector3Int> positions;
        private int width;
        private int height;
        private RuleTile ruleHillTile;
        private GridManager grid;
        private Tilemap hillTileMap;

        private SpawnManager spawner => SpawnManager.Instance;

        public int Width => width;
        public int Height => height;
        public List<Vector3Int> Positions => positions;

        public Hill(HillDataCollection data, Tilemap hillTile)
        {
            grid = GridManager.Instance;
            hillTileMap = hillTile;
            if (positions == null) positions = new List<Vector3Int>();
            else positions.Clear();
            HillData hillData = data.GetData();
            width = hillData.Width;
            height = hillData.Height;
            ruleHillTile = hillData.RuleTile;

        }

        public bool IsAvailable(Vector3Int pos, List<Vector3Int> spawnPos)
        {
            if (grid.GetTile(pos) == null || grid.GetTile(pos).IsOccupied) return false;

            int x = pos.x;

            for (int i = 0; i < width; i++)
            {
                int y = pos.y;

                for (int j = 0; j < height; j++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    if (grid.GetTile(position) == null
                        || grid.GetTile(position).IsOccupied ||
                        spawnPos.Contains(position)) return false;
                    y++;
                }

                x++;
            }

            return true;
        }

        public void BuildHill(Vector3Int pos, List<Vector3Int> spawnPos)
        {
            if (!IsAvailable(pos, spawnPos)) return;

            int x = pos.x;

            for (int i = 0; i < width; i++)
            {
                int y = pos.y;

                for (int j = 0; j < height; j++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    hillTileMap.SetTile(position, ruleHillTile);
                    TileCustom tileCs = grid.GetTile(position);
                    tileCs.IsOccupied = true;
                    positions.Add(position);
                    y++;
                }
                x++;
            }
        }


    }
}

