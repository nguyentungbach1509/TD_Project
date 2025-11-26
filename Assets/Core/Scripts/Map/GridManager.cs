using Game.Scripts.TileController.Mechanic;
using SubScripts.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.Map.Mechanic
{
    public enum ETile
    {
        Ground,
        Hover,
    }

    public class GridManager : SingletonBase<GridManager>
    {
        [Header("Ground Tile Settings")]
        [SerializeField] Grid grid;
        [SerializeField] RuleTile ruleTile;
        [SerializeField] Tilemap groundTile;
        [SerializeField] Tilemap hoverTile;
        [SerializeField] Tilemap buildHoverTile;

        [Header("Respawn Tile Settings")]
        [SerializeField] Tilemap respawnTileMap;
        [SerializeField] RuleTile spawnRuleTile;
        [SerializeField] Tile hover;

        private Dictionary<Vector3Int, TileCustom> groundTileDict;
        private Dictionary<Vector3Int, TileCustom> respawnTileDict;

        public void Init()
        {
            groundTileDict = new Dictionary<Vector3Int, TileCustom>();
            respawnTileDict = new Dictionary<Vector3Int, TileCustom>();
        }

        public void SetHover(Vector3Int position, Tile tile)
        {
            hoverTile.SetTile(position, tile);
        }

        public void ClearHoverTile() => hoverTile.ClearAllTiles();

        public void SetGround(Vector3Int position)
        {
            groundTile.SetTile(position, ruleTile);
            if (respawnTileDict.TryGetValue(position, out var tile))
            {
                groundTileDict[position] = tile;
                return;
            }
            groundTileDict.Add(position, new TileCustom(position, ETile.Ground));
        }

        public void SetRespawnArea(Vector3Int position, bool isBorder=false)
        {
            if (isBorder) respawnTileMap.SetTile(position, null);
            else respawnTileMap.SetTile(position, spawnRuleTile);
            if(groundTileDict.TryGetValue(position, out var value))
            {
                value.IsOccupied = true;
                respawnTileDict[position] = value;
                return;
            }
            TileCustom tile = new TileCustom(position, ETile.Ground, true);
            respawnTileDict.Add(position, tile);
        }

        public TileCustom GetTile(Vector3Int position)
        {
            if(groundTileDict.TryGetValue(position, out TileCustom tile)) return tile;
            return null;
        }

        public Dictionary<Vector3Int, TileCustom> GroundDict => groundTileDict;
        public Vector3 GridToWorld(Vector3Int position) => grid.CellToWorld(position);
        public Vector3Int WorldToGrid(Vector3 position) => grid.WorldToCell(position);

        public void SetBuildHoverTile(Vector3Int pos)
        {
            buildHoverTile.SetTile(pos, hover);
        }

        public void ClearBuildHoverTile(Vector3Int pos) 
        {
            buildHoverTile.SetTile(pos, null);
        }


        public void ClearMouseHoverTile() => hoverTile.ClearAllTiles();
        public void SetMouseHover(Vector3Int pos)
        {
            ClearHoverTile();
            hoverTile.SetTile(pos, hover);
        }
    }

}

