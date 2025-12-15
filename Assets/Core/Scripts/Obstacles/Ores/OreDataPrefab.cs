using Game.Scripts.ObstacleResource;
using UnityEngine;

[CreateAssetMenu(fileName = "OreDataPrefab", menuName = "Spawner/Obstacles/Ores/Prefab")]
public class OreDataPrefab : ScriptableObject
{
    [SerializeField] string key;
    [SerializeField] Ore prefab;

    public string Key => key;
    public Ore Prefab => prefab;
}
