using Game.Scripts.Map.Mechanic;
using Game.Scripts.ObstacleResource;
using Game.Scripts.TileController.Mechanic;
using Subscripts;
using Subscripts.Extensions;
using Subscripts.Spawn;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.Map.Hills
{
    public class Hill
    {
        private List<Vector3Int> positions;
        private List<Vector3Int> borders;
        private Dictionary<Vector3Int, Vector3Int> corners;
        private Vector3Int[] directions = new Vector3Int[4]
        {
            Vector3Int.left, Vector3Int.right, Vector3Int.up, Vector3Int.down
        };
        private int width;
        private int height;
        private HillRuleTile ruleHillTile;
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

            positions ??= new List<Vector3Int>();
            positions.Clear();

            //??= kiem tra bien co null khong thi se khoi tao cho no
            // con khong thi thoi
            borders ??= new();
            borders.Clear();

            corners ??= new();
            corners.Clear();

            var hillData = data.GetData();
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

            ruleHillTile.borderList.Clear();
            int x = pos.x;

            for (int i = 0; i < width; i++)
            {
                int y = pos.y;

                for (int j = 0; j < height; j++)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    hillTileMap.SetTile(position, ruleHillTile);
                    hillTileMap.RefreshTile(position);
                    TileCustom tileCs = grid.GetTile(position);
                    tileCs.IsOccupied = true;
                    positions.Add(position);
                    y++;
                }
                x++;
            }
           
            hillTileMap.RefreshAllTiles();
            PlaceTrees();
            PlaceOres();
        }


        #region Border/Corner Methods 
        private void StoreColliderTile()
        {
            borders.Clear();
            foreach (var pos in positions)
            {
                if (ruleHillTile.IsBorderTile(pos, hillTileMap))
                {
                    TileCustom tc = grid.GetTile(pos);
                    tc.IsWalkable = false;  // border
                    borders.Add(pos);
                }
            }
        }

        private void StoreCornerTile()
        {
            corners.Clear();

            int minX = int.MaxValue;
            int maxX = int.MinValue;
            int minY = int.MaxValue;
            int maxY = int.MinValue;

            foreach (var p in positions)
            {
                if (p.x < minX) minX = p.x;
                if (p.x > maxX) maxX = p.x;
                if (p.y < minY) minY = p.y;
                if (p.y > maxY) maxY = p.y;
            }

            Vector3Int leftUp = new Vector3Int(minX, maxY, 0);
            Vector3Int rightUp = new Vector3Int(maxX, minY, 0);
            Vector3Int leftDown = new Vector3Int(minX, minY, 0);
            Vector3Int rightDown = new Vector3Int(maxX, minY, 0);

            corners.Add(Vector3IntExt.LeftUp * -2, leftUp);
            //corners.Add(Vector3IntExt.RightUp * -2, rightUp);
            //corners.Add(Vector3IntExt.LeftDown * -2, leftDown);
            //corners.Add(Vector3IntExt.RightDown * -2, rightDown);
        }

        #endregion

        #region Place Trees

        private Vector3Int GetInnerTreePosition(Vector3Int pos)
        {
            for(int i = 0; i < directions.Length; i++)
            {
                Vector3Int outPos = pos + directions[i];

                if(!positions.Contains(outPos))
                {
                    return pos - directions[i];
                }
            }

            return pos;
        }

        private void PlaceTrees()
        {
            StoreColliderTile();

            HashSet<Vector3Int> treePositions = new HashSet<Vector3Int>();

            foreach (var pos in borders)
            {
                Vector3Int treePos = GetInnerTreePosition(pos);
                if(Random.value < .45f && !borders.Contains(treePos))treePositions.Add(treePos);
            }

            // spawn 1 lần duy nhất
            foreach (var treePos in treePositions)
            {
                Vector3 spawnPos = grid.GridToWorld(treePos);
                TreeSource tree = spawner.TreeSpawner.SpawnTree(spawnPos, Quaternion.identity);
                TileCustom tileCs = grid.GetTile(treePos);
                tileCs.SetObstacle(tree);
                tree.SetPlace(treePos);
            }
        }
        #endregion

        #region Place Ores
        private void PlaceOres()
        {
            StoreCornerTile();
            int indexRandom = Random.Range(0, corners.Count);
            int i = 0;
            foreach(var kv in corners)
            {
                if(indexRandom == i)
                {
                    Vector3Int diagonalOppoisite = kv.Value + kv.Key;
                    Vector3 spawnPos = grid.GridToWorld(diagonalOppoisite);
                    Ore ore = spawner.OreSpawner.SpawnOre(Constants.GoldOre, spawnPos);
                    TileCustom tileCs = grid.GetTile(diagonalOppoisite);
                    tileCs.SetObstacle(ore);
                    ore.SetPlace(diagonalOppoisite);
                }

                i++;
            }
        }
        #endregion
    }

}

