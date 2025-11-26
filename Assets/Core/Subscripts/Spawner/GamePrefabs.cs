using Game.Scripts.BuildingLogic.Data;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePrefabs", menuName = "Spawner/Prefab/GamePrefabs")]
public class GamePrefabs : ScriptableObject
{
    [Header("Build Prefabs")]
    [SerializeField] public BuildingDataCollection BuildingPrefabs;
}
