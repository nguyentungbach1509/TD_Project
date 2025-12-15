using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrePrefabCollection", menuName = "Spawner/Obstacles/Ores/PrefabCollection")]
public class OrePrefabCollection : ScriptableObject
{
    [SerializeField] List<OreDataPrefab> prefabs;
    public List<OreDataPrefab> Prefabs => prefabs;

}
