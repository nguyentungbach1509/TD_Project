using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using Game.Scripts.TileController.Mechanic;
using NUnit.Framework;
using SubScripts.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class PlayerStateController : StateController
    {
        private PlayerController player;
        private PlayerInputController input => PlayerInputController.Instance;
        private Vector3Int target;

        public Vector3Int Target => target;

        public PlayerStateController(CharacterBase character) : base(character)
        {
            input.OnMouseRightClick -= SetTarget;
            input.OnMouseRightClick += SetTarget;
            input.OnMouseLeftClick -= SetTarget;
            input.OnMouseLeftClick += SetTarget;
            
            player = character as PlayerController;
            currentState = new IdleState(player);
            currentState.Enter();
        }

        private void SetTarget(Vector3Int target)
        {
            TileCustom tileCustom = grid.GetTile(target);
            Obstacle obstacle = tileCustom.GetObstacle();
            if (obstacle != null)
            {
                this.target = obstacle.FindBestInteractionTile();
            }
            else this.target = target;
            ChangeState(AnimationKey.Move);
        }

        protected override State DetectState(AnimationKey key)
        {
            State state = new IdleState(player);
            switch (key)
            {
                case AnimationKey.Move:
                    state = new MoveState(player);
                    break;
                case AnimationKey.Build:
                    state = new BuildState(player);
                    break;
                case AnimationKey.Hit:
                    state = new HitState(player);
                    break;
                case AnimationKey.Chop:
                    state = new ChopState(player);      
                    break;
                case AnimationKey.Die:
                    state = new DieState(player);
                    break;
            }

            cachedState.Add(key, state);
            return state;
        }
    }
}

