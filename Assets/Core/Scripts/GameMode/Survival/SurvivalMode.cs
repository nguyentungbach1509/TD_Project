using Game.Scripts.BuildingLogic;
using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using NUnit.Framework;
using Subscripts;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Scripts.GamePlay
{
    public class SurvivalMode : GameMode
    {
        private BuildManager buildManager => BuildManager.Instance;
        
        public static SurvivalMode Instance => GetInstance<SurvivalMode>();

        public override void Init()
        {
            base.Init();
            spawnManager.Init();
            gridManager.Init();
            mapGenerator.Init();
            buildManager.Init();
            cameraController.Init();
        }

        public override void StartGame()
        {
            spawnManager.BuilderSpawner.SpawnBuilder(UnitKey.Player, Vector3Int.zero, Quaternion.identity);
            Selected(Vector3Int.zero);
        }

        public override void UpdateGame()
        {
            if(selectedUnit != null) selectedUnit.UpdateCharacter();
            buildManager.UpdateBuilder();
        }

        public override void LateUpdateGame()
        {
            cameraController.FollowPlayer();
        }

        
    }
}

