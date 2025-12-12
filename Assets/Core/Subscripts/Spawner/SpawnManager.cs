using Game.Scripts.BuildingLogic;
using Game.Scripts.ObstacleResource;
using Game.Scripts.UI;
using Mono.Cecil;
using SubScripts.Singleton;
using UnityEngine;

namespace Subscripts.Spawn
{
    public class SpawnManager : SingletonBase<SpawnManager>
    {
        [SerializeField] GamePrefabs gamePrefabs;
        [SerializeField] UIPrefabs uiPrefabs;

        private BuildingSpawner buildingSpawner;
        private TreeSpawner treeSpawner;

        #region UISpawner
        private ResourceTxtSpawner resourceTxtSpawner;
        #endregion

        public BuildingSpawner BuildingSpawner => buildingSpawner;
        public TreeSpawner TreeSpawner => treeSpawner;
        public ResourceTxtSpawner ResourceTxtSpawner => resourceTxtSpawner;

        public void Init()
        {
            buildingSpawner = new BuildingSpawner(gamePrefabs);
            treeSpawner = new TreeSpawner(gamePrefabs);
            resourceTxtSpawner = new ResourceTxtSpawner(uiPrefabs);
        }
    }
}

