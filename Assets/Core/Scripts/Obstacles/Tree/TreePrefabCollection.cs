using System.Collections.Generic;
using UnityEngine;


namespace Game.Scripts.ObstacleResource
{
    [CreateAssetMenu(fileName = "TreePrefabCollection", menuName = "Spawner/Obstacles/Tree/PrefabCollection")]
    public class TreePrefabCollection : ScriptableObject
    {
        public List<TreePrefabData> Prefabs;
    }
}

