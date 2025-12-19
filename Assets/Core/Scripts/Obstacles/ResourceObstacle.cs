using Game.Scripts.Map.Obstacles;
using System;
using UnityEngine;

namespace Game.Scripts.ObstacleResource
{
    public class ResourceObstacle : Obstacle
    {
        [SerializeField] protected ResourceObstacleHUD hud;
        [SerializeField] protected int eachHarvest;
        
        protected string key;
        
        public string Key => key;
        private Action<int> onHarvestTextChange;

        public virtual void Init(string key)
        {
            inputCtrl.OnMouseRightClick -= Interact;
            inputCtrl.OnMouseRightClick += Interact;

            onHarvestTextChange -= hud.EffectResourceText;
            onHarvestTextChange += hud.EffectResourceText;

            positions = new();
            this.key = key;
        }
        

        public override void Interact(Vector3Int pos)
        {
            if (!positions.Contains(pos)) return;
            mode.SelectedUnit.CurrentObstacle = this;
        }

        public override void SetPlace(Vector3Int pos)
        {
            positions.Add(pos);
        }

        public void OnHarvestResources()
        {
            onHarvestTextChange?.Invoke(eachHarvest);
        }
    }

}

