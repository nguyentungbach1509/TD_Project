using DG.Tweening;
using Game.Scripts.GamePlay;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.Controller;
using SubScripts.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class MoveState : BuilderState
    {
        private List<Vector3Int> path;
        private Sequence moveSequence;
        private Vector3Int currentPos;
        public MoveState(BuilderController player) : base(player)
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
            else currentPos = path[0];

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
                UnitController.UpdatePosition(currentPos, State.Target);
                // nếu sau khi đi xong mà gặp obstacle → đổi state  
                if (player.CurrentObstacle != null && player.InInteractRange())
                {
                    StateByObstacle();
                    return;
                }
                State.ChangeState(AnimationKey.Idle);
            });
        }

        private void StateByObstacle()
        {
            EObstacleType type = player.CurrentObstacle.ObstacleType;
            switch(type)
            {
                case EObstacleType.Chopable:
                    State.ChangeState(AnimationKey.Chop);
                    break;
                case EObstacleType.Miningable:
                    State.ChangeState(AnimationKey.Mining);
                    break;
                case EObstacleType.Buildable:
                    State.ChangeState(AnimationKey.Build);
                    break;
            }
        }

        
        public override void Exit()
        {
            
        }
    }
}

