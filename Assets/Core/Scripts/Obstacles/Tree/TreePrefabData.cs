using UnityEngine;

namespace Game.Scripts.ObstacleResource
{
    [CreateAssetMenu(fileName = "TreePrefabData", menuName = "Spawner/Obstacles/Tree/Prefab")]
    public class TreePrefabData : ScriptableObject
    {
        [SerializeField] string key;
        [SerializeField] TreeSource prefab;
        [Range(0, .5f)]
        [SerializeField] float randomPercent;

        public string Key => key;
        public TreeSource Prefab => prefab;
        public float RandomPercent => randomPercent;    
    } 
}


