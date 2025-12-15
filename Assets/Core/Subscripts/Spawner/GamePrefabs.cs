using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.ObstacleResource;
using Game.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePrefabs", menuName = "Spawner/Prefab/GamePrefabs")]
public class GamePrefabs : ScriptableObject
{
    [Header("Builder Prefabs")]
    [SerializeField] public UnitPrefabCollection BuilderPrefabs;

    [Header("Build Prefabs")]
    [SerializeField] public BuildingDataCollection BuildingPrefabs;

    [Header("Tree Prefabs")]
    [SerializeField] public TreePrefabCollection TreePrefabs;

    [Header("Ore Prefabs")]
    [SerializeField] public OrePrefabCollection OrePrefabs;
}
