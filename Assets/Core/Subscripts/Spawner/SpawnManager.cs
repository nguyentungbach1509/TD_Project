using Game.Scripts.BuildingLogic;
using Game.Scripts.ObstacleResource;
using SubScripts.Singleton;
using UnityEngine;

namespace Subscripts.Spawn
{
    public class SpawnManager : SingletonBase<SpawnManager>
    {
        [SerializeField] GamePrefabs gamePrefabs;

        private BuildingSpawner buildingSpawner;
        private TreeSpawner treeSpawner;

        public BuildingSpawner BuildingSpawner => buildingSpawner;
        public TreeSpawner TreeSpawner => treeSpawner;

        public void Init()
        {
            buildingSpawner = new BuildingSpawner(gamePrefabs);
            treeSpawner = new TreeSpawner(gamePrefabs);
        }
    }
}

