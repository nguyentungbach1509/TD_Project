using Game.Scripts.ObstacleResource;
using Game.Scripts.StatsCharacter;
using SubScripts;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class ChopState : BuilderState
    {
        private ResourceObstacle resource;

        public ChopState(CharacterBase character) : base(character)
        {
            anim.RegisterAnimationEvent(AnimationKey.Chop, AnimationEventType.Hit, OnChop);
        }

        public override void Enter()
        {
            Debug.Log("Chop State");
            resource = player.CurrentObstacle as ResourceObstacle;
            anim.PlayAnimation(AnimationKey.Chop);
        }

        private void OnChop()
        {
            resource.OnHarvestResources();
        }


        public override void Execute()
        {
            if(player.CurrentObstacle == null)
            {
                State.ChangeState(AnimationKey.Idle);
                return;
            }
        }

        public override void Exit()
        {

        }
    }
}
