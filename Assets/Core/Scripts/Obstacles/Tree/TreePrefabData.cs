using UnityEngine;

namespace Game.Scripts.ObstacleResource
{
    [CreateAssetMenu(fileName = "TreePrefabData", menuName = "Spawner/Obstacles/Tree/Prefab")]
    public class TreePrefabData : ScriptableObject
    {
        [SerializeField] string key;
        [SerializeField] Tree prefab;

        public string Key => key;
        public Tree Prefab => prefab;
    } 
}


