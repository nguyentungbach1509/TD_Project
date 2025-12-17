using Game.Scripts.BuildingLogic;
using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.ObstacleResource;
using Game.Scripts.Player;
using Game.Scripts.UI;
using Game.Scripts.UI.HUD;
using Mono.Cecil;
using NUnit.Framework;
using SubScripts.Singleton;
using System.Collections.Generic;
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
        private SlotHUDSpawner slotHUDSpawner;
        #endregion

        public BuildingSpawner BuildingSpawner => buildingSpawner;
        public TreeSpawner TreeSpawner => treeSpawner;
        public OreSpawner OreSpawner => oreSpawner;
        public BuilderSpawner BuilderSpawner => builderSpawner;
        
        public ResourceTxtSpawner ResourceTxtSpawner => resourceTxtSpawner;
        public SlotHUDSpawner SlotHUDSpawner => slotHUDSpawner;

        public void Init()
        {
            BuildingController.Init(gamePrefabs);
            builderSpawner = new BuilderSpawner(gamePrefabs);
            buildingSpawner = new BuildingSpawner(gamePrefabs);
            treeSpawner = new TreeSpawner(gamePrefabs);
            oreSpawner = new OreSpawner(gamePrefabs);
            resourceTxtSpawner = new ResourceTxtSpawner(uiPrefabs);
            slotHUDSpawner = new SlotHUDSpawner(uiPrefabs);
        }
    }
}

