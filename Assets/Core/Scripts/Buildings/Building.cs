using Game.Scripts.BuidlingLogic.Data;
using Game.Scripts.BuidlingLogic.WorldUI;
using Game.Scripts.GamePlay;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.Controller;
using Game.Scripts.TileController.Mechanic;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.BuidlingLogic
{
    public class Building : Obstacle
    {
        [SerializeField] private BuildingData data;
        [SerializeField] private BuildingModel model;
        [SerializeField] private BuildingHUD hud;

        private BuildingStats stats;
        private float currentProgress;

        private bool inProgressing;
        private bool isDone;

        private Coroutine progressingCoroutine;

        private bool isInit;

        private GridManager gridManager => GridManager.Instance;
        private SurvivalMode survivalMode => SurvivalMode.Instance;

        public BuildingHUD Hud => hud;

        private Action<float> OnProgressChange;

        public override void Init()
        {
            hud.Init();
            
            OnProgressChange -= hud.ProgressBar.UpdateProgressBar;
            OnProgressChange += hud.ProgressBar.UpdateProgressBar;

            stats = new BuildingStats(data, hud);
            inProgressing = true;
            isInit = true;
        }

        public override void SetPlace(Vector3Int pos)
        {
            TileCustom tile = gridManager.GetTile(pos);
            if (tile.IsOccupied) return;
            tile.SetObstacle(this);
            StartBuild();
        }

        public virtual void FollowMouseHover(Vector3Int pos)
        {
            transform.position = pos;
            TileCustom tile = gridManager.GetTile(pos);
            model.WarningSprite(tile.IsOccupied);
        }

        public override void Interact()
        {
            
        }

        private void StartBuild()
        {
            if (progressingCoroutine != null) StopCoroutine(progressingCoroutine);
            progressingCoroutine = StartCoroutine(StartProgress());

            IEnumerator StartProgress()
            {
                while(InInteractRange() && currentProgress < 100f)
                {
                    inProgressing = true;
                    yield return new WaitForSeconds(.15f);
                    currentProgress = Mathf.Clamp(currentProgress + 1, 0, 100f);
                    OnProgressChange?.Invoke(currentProgress / 100f);
                }
                
                inProgressing = false;
                isDone = true;
                yield return null;
            }
        }

        protected override bool InInteractRange()
        {
            PlayerController player = survivalMode.Player;
            float distance = Vector3.Distance(player.transform.position, transform.position);
            return distance <= interactRange;
        }
    }

}
