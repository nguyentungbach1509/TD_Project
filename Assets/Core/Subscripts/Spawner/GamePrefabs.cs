using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.ObstacleResource;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePrefabs", menuName = "Spawner/Prefab/GamePrefabs")]
public class GamePrefabs : ScriptableObject
{
    [Header("Build Prefabs")]
    [SerializeField] public BuildingDataCollection BuildingPrefabs;

    [Header("Tree Prefabs")]
    [SerializeField] public TreePrefabCollection TreePrefabs;

}
