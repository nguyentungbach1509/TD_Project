using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.Map.Mechanic;
using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Map.Obstacles
{

    public abstract class Obstacle : PoolableComponent
    {
        [SerializeField] protected float interactRange;

        protected List<Vector3Int> positions;
        protected GridManager grid => GridManager.Instance;
        protected List<Vector3Int> borders;

        public List<Vector3Int> Positions => positions;
        public List<Vector3Int> Borders => borders;
        public float InteractRange => interactRange;   
        public Vector3Int GridPos => grid.WorldToGrid(transform.position);

        public abstract void SetPlace(Vector3Int pos);

        public abstract void Interact(Vector3Int pos);
        public abstract bool InInteractRange();
        
        public void SetBorders()
        {
            for(int i = 0; i < positions.Count; i++)
            {

            }
        }
    }
}

