using SubScripts.Pooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Map.Obstacles
{

    public abstract class Obstacle : PoolableComponent
    {
        [SerializeField] protected float interactRange;

        protected List<Vector3Int> positions;

        public List<Vector3Int> Positions => positions;
        public float InteractRange => interactRange;    

        public virtual void Init()
        {

        }

        public abstract void SetPlace(Vector3Int pos);

        public abstract void Interact(Vector3Int pos);
        protected abstract bool InInteractRange();
        
    }
}

