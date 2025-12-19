
using Subscripts;
using UnityEngine;


namespace Game.Scripts.GamePlay
{
    public class SurvivalMode : GameMode
    {
        
        public static SurvivalMode Instance => GetInstance<SurvivalMode>();

        public override void Init()
        {
            base.Init();
            spawnManager.Init();
            gridManager.Init();
            mapGenerator.Init();
            buildManager.Init();
            cameraController.Init();
            uiManger.Init();
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

