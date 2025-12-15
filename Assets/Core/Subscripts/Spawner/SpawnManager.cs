using Game.Scripts.BuildingLogic;
using Game.Scripts.ObstacleResource;
using Game.Scripts.Player;
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
        private OreSpawner oreSpawner;
        private BuilderSpawner builderSpawner;

        #region UISpawner
        private ResourceTxtSpawner resourceTxtSpawner;
        #endregion

        public BuildingSpawner BuildingSpawner => buildingSpawner;
        public TreeSpawner TreeSpawner => treeSpawner;
        public OreSpawner OreSpawner => oreSpawner;
        public BuilderSpawner BuilderSpawner => builderSpawner;
        public ResourceTxtSpawner ResourceTxtSpawner => resourceTxtSpawner;

        public void Init()
        {
            builderSpawner = new BuilderSpawner(gamePrefabs);
            buildingSpawner = new BuildingSpawner(gamePrefabs);
            treeSpawner = new TreeSpawner(gamePrefabs);
            oreSpawner = new OreSpawner(gamePrefabs);
            resourceTxtSpawner = new ResourceTxtSpawner(uiPrefabs);
        }
    }
}

