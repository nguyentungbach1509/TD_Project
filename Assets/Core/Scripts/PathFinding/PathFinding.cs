using Game.Scripts.Map.Mechanic;
using Game.Scripts.TileController.Mechanic;
using SubScripts.Singleton;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Path
{
    public class PathFinding : SingletonBase<PathFinding>
    {
        private GridManager grid => GridManager.Instance;
        
        private static readonly Vector3Int[] directions = new Vector3Int[]
        {
            Vector3Int.left, Vector3Int.right, Vector3Int.up, Vector3Int.down,
        };

        private List<TileCustom> GetNeighbours(Vector3Int pos)
        {
            List<TileCustom> results = new();
            for(int i = 0; i < directions.Length; i++)
            {
                Vector3Int neighbour = pos + directions[i];
                if(grid.GroundDict.ContainsKey(neighbour) && grid.GetTile(neighbour).IsWalkable)
                {
                    results.Add(grid.GetTile(neighbour));
                }
            }
            return results;
        }

        private int DistanceMahattan(TileCustom a, TileCustom b)
        {
            return Mathf.Abs(a.Position.x - b.Position.x)
                     + Mathf.Abs(a.Position.y - b.Position.y);
        }

        public List<Vector3Int> GetPath(Vector3Int start, Vector3Int end)
        {
            TileCustom startTile = grid.GetTile(start);
            TileCustom endTile = grid.GetTile(end);

            if(startTile == null || endTile == null) return new List<Vector3Int>();

            foreach(var tile in grid.GroundDict.Values)
            {
                tile.Node.G = int.MaxValue;
                tile.Node.H = 0;
                tile.Parent = null;
            }

            List<TileCustom> openList = new(); //Tile dang cho` xet
            HashSet<TileCustom> closedList = new(); //Cac tile da set xong

            startTile.Node.G = int.MaxValue;
            startTile.Node.H = DistanceMahattan(startTile, endTile);
            startTile.Parent = null;
            openList.Add(startTile);

            while(openList.Count > 0)
            {
                //Lay tile co F nho nhat
                TileCustom current = openList.OrderBy(tc => tc.Node.F).First();

                if(current == endTile) return ReconstructPath(startTile, endTile);

                openList.Remove(current);
                closedList.Add(current);

                List<TileCustom> neighbours = GetNeighbours(current.Position);

                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (closedList.Contains(neighbours[i])) continue;

                    if (!neighbours[i].IsWalkable) continue;

                    int newCost = current.Node.G + 1;

                    if (newCost < neighbours[i].Node.G)
                    {
                        neighbours[i].Node.G = newCost;
                        neighbours[i].Node.H = DistanceMahattan(neighbours[i], endTile);
                        neighbours[i].Parent = current;

                        if (!openList.Contains(neighbours[i])) openList.Add(neighbours[i]);

                    }
                }
            }

            return new List<Vector3Int>();
        }
        
        private List<Vector3Int> ReconstructPath(TileCustom start, TileCustom end)
        {
            List<Vector3Int> path = new();
            TileCustom current = end;
            while(current != start)
            {
                path.Add(current.Position);
                current = current.Parent;
            }

            path.Add(current.Position);
            path.Reverse();
            return path;
        }
    }
}

