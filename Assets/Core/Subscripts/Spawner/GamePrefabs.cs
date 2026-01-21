using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.ObstacleResource;
using Game.Scripts.Player;
using Game.Scripts.Projectiles;
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

    [Header("Projectiles")]
    [SerializeField] public ProjectileSpawnData ProjectilePrefabs;

    [Header("Player Prefab")]
    [SerializeField] public UnitDataPrefab PlayerPrefab;
    
    [Header("Unit Prefabs")]
    [SerializeField] public UnitPrefabCollection UnitPrefabs;
}
