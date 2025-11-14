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

            positions = new();
            stats = new BuildingStats(data, hud);
            inProgressing = true;
            isInit = true;
        }

        public override void SetPlace(Vector3Int pos)
        {
            if (!IsAvailableTile(pos)) return;
            for(int i = 0; i < positions.Count; i++)
            {
                TileCustom tile = gridManager.GetTile(positions[i]);
                tile.SetObstacle(this);
            }
            StartBuild(pos);
        }

        public virtual void FollowMouseHover(Vector3Int pos)
        {
            positions.Clear();
            transform.position = gridManager.GridToWorld(pos);
            model.WarningSprite(!IsAvailableTile(pos));
        }

        public override void Interact(Vector3Int pos)
        {
            if (!positions.Contains(pos)) return;
            if (inProgressing) StartBuild(pos);
        }

        private bool IsAvailableTile(Vector3Int pos)
        {
            for(int row = 0; row < stats.Size.Height; row++)
            {
                for(int col = 0; col < stats.Size.Width; col++)
                {
                    Vector3Int tilePos = new Vector3Int(pos.x + col, pos.y + row, 0);
                    TileCustom tile = gridManager.GetTile(tilePos);
                    if(tile.IsOccupied || tile == null)
                    {
                        positions.Clear();
                        return false;
                    }
                    positions.Add(tilePos);
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
