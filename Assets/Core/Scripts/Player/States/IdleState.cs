using Game.Scripts.Player.Controller;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class IdleState : BuilderState
    {
        public IdleState(BuilderController player) : base(player)
        {
        }

        public override void Enter()
        {
            Debug.Log("Idle State");
            anim.PlayAnimation(AnimationKey.Idle);
        }

        public override void Execute()
        {
            Vector2 moveVector = input["Move"].ReadValue<Vector2>().normalized;
            player.ChangeSide(moveVector);
            if(moveVector.sqrMagnitude > 0.01f)
            {
                State.ChangeState(AnimationKey.Move);
                return;
            }
        }

        public override void Exit()
        {
            
        }
    }
}

