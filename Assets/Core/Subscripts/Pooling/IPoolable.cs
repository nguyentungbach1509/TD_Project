using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubScripts.Pooling
{
    public interface IPoolable
    {
        void OnSpawn();
        void OnDespawn();
    }
}

