using Game.Scripts.Player.Controller;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class MoveState : PlayerState
    {
        public MoveState(PlayerController player) : base(player)
        {
        }

        public override void Enter()
        {
            Debug.Log("Move State");
            anim.PlayAnimation(AnimationKey.Move);
        }

        public override void Execute()
        {
            if(player.CurrentObstacle != null && player.CurrentObstacle.InInteractRange())
            {
                State.ChangeState(AnimationKey.Chop);
                return;
            }

            Vector2 moveVector = input["Move"].ReadValue<Vector2>().normalized;
            Vector2 newPosition = Rb.position + moveVector * stats.MoveSpeed * Time.deltaTime;
            Rb.MovePosition(newPosition);
            player.ChangeSide(moveVector);
            
            if(moveVector.normalized == Vector2.zero) 
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

