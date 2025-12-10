using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.BuildingLogic.WorldUI;
using Game.Scripts.GamePlay;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.Controller;
using Game.Scripts.TileController.Mechanic;
using Subscripts.Spawn;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.BuildingLogic
{
    public class Building : Obstacle
    {
        [SerializeField] private BuildingModel model;
        [SerializeField] private BuildingHUD hud;
        [SerializeField] private float buildTime;

        private BuildingStats stats;
        private float currentProgress;

        private bool inProgressing;
        private bool inUpgradeProcessing;

        private Coroutine progressingCoroutine;
        private Coroutine upgradeCoroutine;
        
        private bool isInit;

        private GridManager gridManager => GridManager.Instance;
        private SurvivalMode survivalMode => SurvivalMode.Instance;
        private BuildManager buildManager => BuildManager.Instance;
        private SpawnManager spawner => SpawnManager.Instance;

        public BuildingHUD Hud => hud;

        public Action<float> OnProgressChange;

        public BuildingStats Stats => stats;
        public BuildingModel Model => model;

        public void Init(BuildingData data)
        {
            currentProgress = 0;
            positions = new();
            hud.ShowProgressBar();
            hud.HideHealthBar();
            stats = new BuildingStats(data, hud);
            model.BlurSprite();
            inProgressing = false;
            isInit = true;
        }

        public void SetupModel(BuildingModel buildModel)
        {
            model = buildModel;
            hud = buildModel.HUD;
            hud.Init();
            OnProgressChange -= hud.ProgressBar.UpdateProgressBar;
            OnProgressChange += hud.ProgressBar.UpdateProgressBar;
        }

        public override void SetPlace(Vector3Int pos)
        {
            for(int i = 0; i < positions.Count; i++)
            {
                TileCustom tile = gridManager.GetTile(positions[i]);
                tile.SetObstacle(this);
                gridManager.SetHover(pos);
            }
            StartBuild(pos);
        }

        public virtual void FollowMouseHover(Vector3Int pos)
        {
            if (inUpgradeProcessing) return;
            positions.Clear();
            transform.position = gridManager.GridToWorld(pos);
            model.WarningSprite(!IsAvailableTile(pos));
        }

        public override void Interact(Vector3Int pos)
        {
            if (!positions.Contains(pos) || inProgressing) return;
            Upgrade();
        }

        public bool IsAvailableTile(Vector3Int pos)
        {
            gridManager.ClearHoverTile();
            positions.Clear();

            //Dua ve vi tri trung tam cua obstacle,
            //thay vi luon bat dau tu tile goc duoi ben trai
            int offsetX = (stats.Size.Width - 1) / 2;
            int offsetY = (stats.Size.Height - 1) / 2;
            centerGridPos = new Vector3Int(pos.x - offsetX, pos.y - offsetY, 0);


            for (int row = 0; row < stats.Size.Height; row++)
            {
                for (int col = 0; col < stats.Size.Width; col++)
                {
                    Vector3Int tilePos = new Vector3Int(centerGridPos.x + col, centerGridPos.y + row, 0);
                    TileCustom tile = gridManager.GetTile(tilePos);

                    if (tile == null || (tile.IsOccupied && !tile.IsWalkable))
                    {
                        positions.Clear();
                        return false;
                    }

                    positions.Add(tilePos);
                    gridManager.SetHover(tilePos);
                }
            }

            return true;
        }


        private void StartBuild(Vector3Int pos)
        {
            if (progressingCoroutine != null) StopCoroutine(progressingCoroutine);
            progressingCoroutine = StartCoroutine(StartProgress());

            IEnumerator StartProgress()
            {
                if(!inProgressing) transform.position = gridManager.GridToWorld(pos);

                float timer = 0f;

                while (timer < buildTime)
                {
                    timer += Time.deltaTime;
                    currentProgress = Mathf.Clamp01(timer / buildTime);
                    OnProgressChange?.Invoke(currentProgress);
                    yield return null;
                }

                // đảm bảo = 100%
                currentProgress = 1f;
                OnProgressChange?.Invoke(currentProgress);

                inProgressing = false;
                model.FixedModel();
                gridManager.ClearHoverTile();
                hud.HideProgressBar();
                hud.ShowHealthBar();
                yield return null;
            }
        }

        public override bool InInteractRange()
        {
            PlayerController player = survivalMode.Player;
            float distance = Vector3.Distance(player.transform.position, transform.position);
            Debug.Log($"DISTANCE: {distance}");
            return distance <= interactRange;
        }

        #region Upgrade Building
        private void Upgrade()
        {
            //check dieu kien du update chua
            RequiredBuilding[] req = stats.Requirements[stats.Level].RequiredBuildings;

            for (int i = 0; i < req.Length; i++)
            {
                if (!buildManager.CurrentBuildings.Contains(req[i]))
                {
                    return;
                }
            }

            //Check dieu kien
            //if(player.Storage.Gold < requirements[level].RequiredGolds && 
            //player.Storage.Lumbers < requirements[Level].RequiredLumbers &&
            //player.Storage.Foods < requirements[level].RequiredFoods) return;

            if (upgradeCoroutine != null) StopCoroutine(upgradeCoroutine);
            upgradeCoroutine = StartCoroutine(UpgradeCoroutine());

            IEnumerator UpgradeCoroutine()
            {
                float timer = 0f;
                inUpgradeProcessing = true;
                hud.ShowProgressBar();

                while (timer < buildTime)
                {
                    timer += Time.deltaTime;
                    currentProgress = Mathf.Clamp01(timer / buildTime);
                    OnProgressChange?.Invoke(currentProgress);
                    yield return null;
                }

                // đảm bảo = 100%
                currentProgress = 1f;
                OnProgressChange?.Invoke(currentProgress);


                //Update neu du dk
                inUpgradeProcessing = false;
                spawner.BuildingSpawner.DespawnModel(this);
                stats.Upgrade(stats.Requirements[stats.Level]);
                model = spawner.BuildingSpawner.SpawnModel(stats.Type, stats.Key, Stats.Level);
                model.transform.SetParent(transform, false);
                model.transform.localPosition = Vector3.zero;
                SetupModel(model);
                hud.HideProgressBar();
                hud.ShowHealthBar();
            }
            
        }
        #endregion
    }

}
