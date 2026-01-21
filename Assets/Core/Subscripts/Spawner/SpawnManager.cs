using Game.Scripts.BuildingLogic;
using Game.Scripts.ObstacleResource;
using Game.Scripts.Player;
using Game.Scripts.Projectiles;
using Game.Scripts.UI;
using Game.Scripts.UI.HUD;
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
        private ProjectileSpawner projectileSpawner;
        
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
        public ProjectileSpawner ProjectileSpawner => projectileSpawner;

        public void Init()
        {
            builderSpawner = new BuilderSpawner(gamePrefabs);
            buildingSpawner = new BuildingSpawner(gamePrefabs);
            treeSpawner = new TreeSpawner(gamePrefabs);
            oreSpawner = new OreSpawner(gamePrefabs);
            resourceTxtSpawner = new ResourceTxtSpawner(uiPrefabs);
            slotHUDSpawner = new SlotHUDSpawner(uiPrefabs);
            projectileSpawner = new ProjectileSpawner(gamePrefabs);
        }
    }
}

