using Game.Scripts.Manager;
using Game.Scripts.Map.Mechanic;
using UnityEngine;

namespace Game.Scripts.GamePlay
{
    public class SurvivalMode : GameMode
    {
        public static SurvivalMode Instance => GetInstance<SurvivalMode>();

        public override void Init()
        {
            gridManager.Init();
            mapGenerator.Init();
            player.Init();
            cameraController.Init(player);
        }

        public override void UpdateGame()
        {
            player.UpdateCharacter();
        }

        public override void LateUpdateGame()
        {
            cameraController.FollowPlayer();
        }

        
    }
}

