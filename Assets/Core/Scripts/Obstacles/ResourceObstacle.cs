using Game.Scripts.GamePlay;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.Controller;
using System;
using UnityEngine;

namespace Game.Scripts.ObstacleResource
{
    public class ResourceObstacle : Obstacle
    {
        [SerializeField] protected ResourceObstacleHUD hud;
        [SerializeField] protected int eachHarvest;
        protected SurvivalMode survivalMode => SurvivalMode.Instance;
        protected PlayerInputController inputCtrl => PlayerInputController.Instance;
        
        protected string key;
        
        public string Key => key;
        public Action<int> OnHarvestTextChange;

        public virtual void Init(string key)
        {
            inputCtrl.OnMouseRightClick -= Interact;
            inputCtrl.OnMouseRightClick += Interact;

            OnHarvestTextChange -= hud.EffectResourceText;
            OnHarvestTextChange += hud.EffectResourceText;

            this.key = key;
            hud.Init();
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

