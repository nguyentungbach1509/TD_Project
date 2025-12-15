using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.GamePlay;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.Controller;
using Game.Scripts.TileController.Mechanic;
using Subscripts;
using Subscripts.Spawn;
using SubScripts.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuildingLogic
{
    public class BuildManager : SingletonBase<BuildManager>
    {
        private GridManager gridManager => GridManager.Instance;
        private SpawnManager spawner => SpawnManager.Instance;
        private PlayerInputController inputCtrl => PlayerInputController.Instance;

        private Building currentBuild;
        private List<Building> listBuildings;
        private HashSet<RequiredBuilding> currentRequirements;
        private SurvivalMode survivalMode => SurvivalMode.Instance;

        public List<Building> Buildings => listBuildings;
        public HashSet<RequiredBuilding> CurrentBuildings => currentRequirements;

        private bool isInit;


        public void Init()
        {
            inputCtrl.OnMouseRightClick -= PlaceBuilding;
            inputCtrl.OnMouseRightClick += PlaceBuilding;

            inputCtrl.OnMouseRightClick -= InteractBuild;
            inputCtrl.OnMouseRightClick += InteractBuild;

            listBuildings = new();
            currentRequirements = new();
            isInit = true;
        }

        public void UpdateBuilder()
        {
            if (!isInit) return;
            SelectBuilding();
            if (currentBuild == null) return;
            currentBuild.FollowMouseHover(inputCtrl.GridMousePos());
        }

        #region Simple To Test
        private void SelectBuilding()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (currentBuild != null)
                {
                    spawner.BuildingSpawner.DespawnBuilding(currentBuild);
                    currentBuild = null;
                }
                currentBuild = spawner.BuildingSpawner.SpawnBuilding(EBuildingType.Wall, BuildingKey.Wall_Up,
                    inputCtrl.GridMousePos(), Quaternion.identity);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (currentBuild != null)
                {
                    spawner.BuildingSpawner.DespawnBuilding(currentBuild);
                    currentBuild = null;
                }
                currentBuild = spawner.BuildingSpawner.SpawnBuilding(EBuildingType.Farm, BuildingKey.Farm,
                    inputCtrl.GridMousePos(), Quaternion.identity);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (currentBuild != null)
                {
                    spawner.BuildingSpawner.DespawnBuilding(currentBuild);
                    currentBuild = null;
                }
                currentBuild = spawner.BuildingSpawner.SpawnBuilding(EBuildingType.Basement, BuildingKey.Basement,
                    inputCtrl.GridMousePos(), Quaternion.identity);
            }

        }
        #endregion

        private void PlaceBuilding(Vector3Int pos)
        {
            if(currentBuild == null) return;
            if (!survivalMode.SelectedUnit.InInteractRange()) return;
            if (!currentBuild.IsAvailableTile(pos)) return;
            currentBuild.SetPlace(pos);
            listBuildings.Add(currentBuild);
            currentRequirements.Add(
                new RequiredBuilding(currentBuild.Stats.Type, currentBuild.Stats.Level));
            currentBuild = null;
            gridManager.ClearHoverTile();
        }
        
        private void InteractBuild(Vector3Int pos)
        {
            TileCustom tile = gridManager.GetTile(pos);
            Obstacle obstacle = tile.GetObstacle();
            if (obstacle == null || obstacle is not Building) return;
            currentBuild = obstacle as Building;
            if (!survivalMode.SelectedUnit.InInteractRange()) return;
            currentBuild.Interact(pos);
            currentBuild = null;
        }
    }
}

