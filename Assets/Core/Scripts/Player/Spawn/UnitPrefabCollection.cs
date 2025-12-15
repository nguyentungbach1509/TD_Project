
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Player
{
    [CreateAssetMenu(fileName = "UnitDataPrefab", menuName = "Spawner/Units/PrefabCollection")]

    public class UnitPrefabCollection : ScriptableObject
    {
        [SerializeField] List<UnitDataPrefab> prefabs;
        public List<UnitDataPrefab> Prefabs => prefabs;
    }
}

