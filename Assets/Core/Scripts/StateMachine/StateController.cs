using Game.Scripts.GamePlay;
using Game.Scripts.Manager;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Path;
using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using Game.Scripts.TileController.Mechanic;
using SubScripts.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public abstract class StateController
    {
        protected CharacterBase character;
        protected State currentState;
        protected GameMode gameMode => GameManager.Instance.CurrentMode;

        protected Vector3Int target;

        protected Dictionary<AnimationKey, State> cachedState;
        protected GridManager grid => GridManager.Instance;
        public Vector3Int Target => target;


        public StateController(CharacterBase character)
        {
            this.character = character;
            cachedState = new();
        }

        public virtual void SetTarget(Vector3Int target)
        {
            if (gameMode.SelectedUnit == null || gameMode.SelectedUnit != character) return;
            TileCustom tileCustom = grid.GetTile(target);
            Obstacle obstacle = tileCustom.GetObstacle();
            if (obstacle != null)
            {
                this.target = obstacle.FindBestInteractionTile();
            }
            else
            {
                character.CurrentObstacle = null;
                this.target = target;
            }

            Vector3 directionMove = (target - character.GridPos);
            Vector2 moveVector = new Vector2(directionMove.x, directionMove.y);
            character.ChangeSide(moveVector.normalized);
            ChangeState(AnimationKey.Move);
        }

        public void UpdateState()
        {
            currentState?.Execute();
        }

        public void ChangeState(AnimationKey key)
        {
            currentState?.Exit();
            if (cachedState.TryGetValue(key, out State state)) currentState = state;
            else currentState = DetectState(key);
            currentState?.Enter();
        }

        protected abstract State DetectState(AnimationKey key);

    }
}

