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
        private Dictionary<Vector3Int, Building> listBuildings;
        private HashSet<RequiredBuilding> currentRequirements;
        private SurvivalMode survivalMode => SurvivalMode.Instance;
        private bool isInit;


        public Dictionary<Vector3Int, Building> Buildings => listBuildings;
        public HashSet<RequiredBuilding> CurrentBuildings => currentRequirements;
        

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
            //SelectBuilding();
            if (currentBuild == null) return;
            currentBuild.FollowMouseHover(inputCtrl.GridMousePos());
        }


        public void SelectBuilding(EBuildingType group, string key)
        {
            if (currentBuild != null)
            {
                spawner.BuildingSpawner.DespawnBuilding(currentBuild);
                currentBuild = null;
            }
            currentBuild = spawner.BuildingSpawner.SpawnBuilding(group, key,
                inputCtrl.GridMousePos(), Quaternion.identity);
        }
        
        private void PlaceBuilding(Vector3Int pos)
        {
            if(currentBuild == null) return;
            survivalMode.SelectedUnit.CurrentObstacle = currentBuild;
            if (!currentBuild.IsAvailableTile(pos)) return;
            currentBuild.SetPlace(pos);
            listBuildings.Add(pos, currentBuild);
            currentRequirements.Add(
                new RequiredBuilding(currentBuild.Stats.Type, currentBuild.Stats.Level));
            currentBuild = null;
            survivalMode.SelectedUnit.CurrentObstacle = null;
            gridManager.ClearHoverTile();
        }
        
        private void InteractBuild(Vector3Int pos)
        {
            TileCustom tile = gridManager.GetTile(pos);
            Obstacle obstacle = tile.GetObstacle();
            if (obstacle == null || obstacle is not Building) return;
            survivalMode.SelectedUnit.CurrentObstacle = obstacle;
            if (!survivalMode.SelectedUnit.InInteractRange()) return;
            //currentBuild.Interact(pos);
        }

        public Building GetBuilding(Vector3Int pos)
        {
            if(listBuildings.TryGetValue(pos, out var building)) return building;
            return null;
        }
    }
}

