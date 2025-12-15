using Game.Scripts.ObstacleResource;
using Game.Scripts.StatsCharacter;
using SubScripts;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class MiningState : BuilderState
    {
        private ResourceObstacle resource;

        public MiningState(CharacterBase character) : base(character)
        {
            anim.RegisterAnimationEvent(AnimationKey.Mining, AnimationEventType.Hit, OnMining);
        }

        public override void Enter()
        {
            Debug.Log("Mining State");
            resource = player.CurrentObstacle as ResourceObstacle;
            anim.PlayAnimation(AnimationKey.Mining);
        }

        private void OnMining()
        {
            resource.OnHarvestResources();
        }
    }
}

