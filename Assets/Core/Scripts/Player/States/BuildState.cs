
using Game.Scripts.BuildingLogic;
using Game.Scripts.Player.Controller;
using SubScripts;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class BuildState : BuilderState
    {
        private Building building;

        public BuildState(BuilderController player) : base(player)
        {
            anim.RegisterAnimationEvent(AnimationKey.Build, AnimationEventType.Hit, OnBuild);
        }

        public override void Enter()
        {
            if (gameMode.SelectedUnit != character || gameMode.SelectedUnit == null) return;
            Debug.Log("Building State");
            building = player.CurrentObstacle as Building;
            anim.PlayAnimation(AnimationKey.Build);
        }

        private void OnBuild()
        {
            building.OnFixBuilding(player.CharacterStats.FixingDmg);
        }
    }
}

