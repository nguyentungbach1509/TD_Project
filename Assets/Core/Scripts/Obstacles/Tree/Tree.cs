using Game.Scripts.GamePlay;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.Controller;
using UnityEngine;

namespace Game.Scripts.ObstacleResource
{
    public class Tree : Obstacle
    {
        private string key;
        private SurvivalMode survivalMode => SurvivalMode.Instance;

        public string Key => key;

        public void Init(string key)
        {
            this.key = key; 
        }

        public override bool InInteractRange()
        {
            PlayerController player = survivalMode.Player;
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Debug.Log($"DISTANCE: {distance}");
            return distance <= interactRange;
        }

        public override void Interact(Vector3Int pos)
        {
            PlayerController player = survivalMode.Player;
            player.CurrentObstacle = this;
        }

        public override void SetPlace(Vector3Int pos)
        {
            
        }
    }
}

