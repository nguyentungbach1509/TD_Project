using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubScripts.Pooling
{
    public class PoolableComponent : MonoBehaviour, IPoolable
    {
        public virtual void OnDespawn()
        {
           
        }

        public virtual void OnSpawn()
        {
           
        }
    }
}


