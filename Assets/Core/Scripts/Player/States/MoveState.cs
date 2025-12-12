using DG.Tweening;
using Game.Scripts.Player.Controller;
using SubScripts.Constants;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game.Scripts.Player.StateMachine
{
    public class MoveState : PlayerState
    {
        private List<Vector3Int> path;
        private Sequence moveSequence;

        public MoveState(PlayerController player) : base(player)
        {
        }

        public override void Enter()
        {
            Debug.Log("Move State");
            path = pathFinder.GetPath(player.GridPos, State.Target);
            
            PathFindingMove();
        }


        private void PathFindingMove()
        {
            if (moveSequence != null && moveSequence.IsActive())
            {
                moveSequence.Kill();
            }

            moveSequence = DOTween.Sequence();
            player.Anim.PlayAnimation(AnimationKey.Move);

            foreach (var step in path)
            {
                moveSequence.Append(
                    player.transform.DOMove(step, 0.25f).SetEase(Ease.Linear)
                );
            }

            moveSequence.OnComplete(() =>
            {
                // nếu sau khi đi xong mà gặp obstacle → chặt
                if (player.CurrentObstacle != null && player.CurrentObstacle.InInteractRange())
                {
                    State.ChangeState(AnimationKey.Chop);
                    return;
                }

                State.ChangeState(AnimationKey.Idle);
            });
        }

        private void FreeMoveInput()
        {
            if (player.CurrentObstacle != null && player.CurrentObstacle.InInteractRange())
            {
                State.ChangeState(AnimationKey.Chop);
                return;
            }

            Vector2 moveVector = input["Move"].ReadValue<Vector2>().normalized;
            Vector2 newPosition = Rb.position + moveVector * stats.MoveSpeed * Time.deltaTime;
            Rb.MovePosition(newPosition);
            player.ChangeSide(moveVector);

            if (moveVector.normalized == Vector2.zero)
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

