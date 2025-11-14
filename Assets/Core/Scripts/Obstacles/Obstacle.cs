using SubScripts.Pooling;
using UnityEngine;

namespace Game.Scripts.Map.Obstacles
{

    public abstract class Obstacle : PoolableComponent
    {
        [SerializeField] protected float interactRange;

        protected Vector3Int[] positions;

        public Vector3Int[] Positions => positions;
        public float InteractRange => interactRange;    

        public virtual void Init()
        {

        }

        public abstract void SetPlace(Vector3Int pos);

        public abstract void Interact();
        protected abstract bool InInteractRange();
        
    }
}

