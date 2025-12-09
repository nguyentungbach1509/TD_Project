using Game.Scripts.Map.Mechanic;
using Game.Scripts.Map.Obstacles;
using UnityEngine;

namespace Game.Scripts.TileController.Mechanic
{
    public class TileCustom
    {
        private Vector3Int position;
        private ETile type;
        private bool isOccupied;
        private bool isWalkable;
        private Obstacle obstacle;

        public Vector3Int Position => position;
        public ETile Type => type;
        
        public bool IsOccupied
        {
            get => isOccupied;
            set => isOccupied = value;
        }

        public TileCustom Parent { get; set; }
        public NodeTile Node { get; set; }

        public bool IsWalkable
        {
            get => isWalkable;
            set => isWalkable = value;
        }

        public TileCustom(Vector3Int position, ETile type, bool occupied=false)
        {
            this.position = position;
            this.type = type;
            isOccupied = occupied;
            isWalkable = true;
            Node = new NodeTile();
            Parent = null;
        }

        public void SetObstacle(Obstacle stuff)
        {
            obstacle = stuff;
            isOccupied = true;
            isWalkable = false;
        }

        public Obstacle GetObstacle() => obstacle;
    }

    public class NodeTile
    {
        public int G; // cost from start
        public int H; // cost to end
        public int F => G + H;

        public NodeTile()
        {
            G = int.MaxValue;
            H = 0;
        }
    }
}

