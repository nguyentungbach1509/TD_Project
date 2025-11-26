using Game.Scripts.BuildingLogic;


namespace Game.Scripts.GamePlay
{
    public class SurvivalMode : GameMode
    {
        private BuildManager buildManager => BuildManager.Instance;

        public static SurvivalMode Instance => GetInstance<SurvivalMode>();

        public override void Init()
        {
            spawnManager.Init();
            gridManager.Init();
            mapGenerator.Init();
            player.Init();
            buildManager.Init();
            cameraController.Init(player);
        }

        public override void UpdateGame()
        {
            player.UpdateCharacter();
            buildManager.UpdateBuilder();
        }

        public override void LateUpdateGame()
        {
            cameraController.FollowPlayer();
        }

        
    }
}

