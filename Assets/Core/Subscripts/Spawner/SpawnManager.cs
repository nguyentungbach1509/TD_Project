using Game.Scripts.BuildingLogic;
using SubScripts.Singleton;
using UnityEngine;

namespace Subscripts.Spawn
{
    public class SpawnManager : SingletonBase<SpawnManager>
    {
        [SerializeField] GamePrefabs gamePrefabs;

        private BuildingSpawner buildingSpawner;

        public BuildingSpawner BuildingSpawner => buildingSpawner;

        public void Init()
        {
            buildingSpawner = new BuildingSpawner(gamePrefabs);
        }
    }
}

