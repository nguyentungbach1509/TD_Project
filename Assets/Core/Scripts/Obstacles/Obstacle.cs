using Game.Scripts.GamePlay;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Path;
using Game.Scripts.Player.Controller;
using SubScripts.Pooling;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Map.Obstacles
{
    public enum EObstacleType
    {
        Chopable, Buildable, Diggable, Miningable
    }

    public abstract class Obstacle : PoolableComponent
    {
        [SerializeField] protected float interactRange;
        [SerializeField] protected EObstacleType obstacleType;
 
        protected List<Vector3Int> positions;
        
        protected Vector3Int centerGridPos;
        
        protected PathFinding pathFinder => PathFinding.Instance;
        protected GridManager grid => GridManager.Instance;
        protected SurvivalMode survivalMode => SurvivalMode.Instance;
        protected PlayerInputController inputCtrl => PlayerInputController.Instance;

        public List<Vector3Int> Positions => positions;
        public float InteractRange => interactRange;   
        public EObstacleType ObstacleType => obstacleType;
        public Vector3Int CenterPos
        {
            get => centerGridPos;
            set => centerGridPos = value;
        }
        
        public abstract void SetPlace(Vector3Int pos);

        public abstract void Interact(Vector3Int pos);
        public abstract bool InInteractRange();


        /// <summary>
        /// Lấy tất cả tile nằm “vòng ngoài” của obstacle
        /// bất kể hình dạng hay bao nhiêu tile chiếm đóng
        /// </summary>
        /// <param name="obstacle"></param>
        /// <returns></returns>
        public List<Vector3Int> GetPerimeterTiles()
        {
            
            // tìm bounding box
            int minX = positions.Min(t => t.x);
            int maxX = positions.Max(t => t.x);
            int minY = positions.Min(t => t.y);
            int maxY = positions.Max(t => t.y);

            List<Vector3Int> perimeter = new List<Vector3Int>();

            // Hàng dưới và trên
            for (int x = minX; x <= maxX; x++)
            {
                perimeter.Add(new Vector3Int(x, minY - 1, 0));
                perimeter.Add(new Vector3Int(x, maxY + 1, 0));
            }

            // Cột trái và phải
            for (int y = minY; y <= maxY; y++)
            {
                perimeter.Add(new Vector3Int(minX - 1, y, 0));
                perimeter.Add(new Vector3Int(maxX + 1, y, 0));
            }

            return perimeter.Distinct().ToList();
        }

        /// <summary>
        /// Lọc tile walkable trong các tile nằm “vòng ngoài” của obstacle
        /// </summary>
        /// <returns></returns>
        public List<Vector3Int> GetWalkableInteractionTiles()
        {
            var perimeter = GetPerimeterTiles();

            return perimeter
                .Where(pos => grid.GetTile(pos).IsWalkable)
                .ToList();
        }

        /// <summary>
        /// Chọn điểm tương tác tốt nhất (gần player nhất) 
        /// để đi đến trong các điểm vòng ngoài thỏa mãn đã tìm được
        /// </summary>
        /// <param name="obstacle"></param>
        /// <param name="playerPos"></param>
        /// <returns></returns>
        public Vector3Int FindBestInteractionTile()
        {
            Vector3Int playerPos = survivalMode.Player.GridPos;
            var candidates = GetWalkableInteractionTiles();

            // sort theo khoảng cách đến player
            candidates.Sort((a, b) =>
                Vector3.Distance(playerPos, a)
                .CompareTo(Vector3.Distance(playerPos, b)));

            // tile đầu tiên mà A* đi được
            foreach (var tile in candidates)
            {
                if (pathFinder.CanReach(playerPos, tile)) return tile;
            }

            return playerPos; // không tìm được
        }

    }
}

