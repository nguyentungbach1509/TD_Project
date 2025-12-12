
using Game.Scripts.BuildingLogic;
using Game.Scripts.Player.Controller;
using SubScripts;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class BuildState : PlayerState
    {
        private Building building;

        public BuildState(PlayerController player) : base(player)
        {
            anim.RegisterAnimationEvent(AnimationKey.Build, AnimationEventType.Hit, OnBuild);
        }

        public override void Enter()
        {
            Debug.Log("Building State");
            building = player.CurrentObstacle as Building;
            anim.PlayAnimation(AnimationKey.Build);
        }

        private void OnBuild()
        {
            building.OnFixBuilding(player.Stats.FixningDmg);
        }
    }
}

