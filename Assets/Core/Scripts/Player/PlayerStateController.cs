using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class PlayerStateController : StateController
    {
        private PlayerController player;

        public PlayerStateController(CharacterBase character) : base(character)
        {
            player = character as PlayerController;
            currentState = new IdleState(player);
            currentState.Enter();
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
                case AnimationKey.Die:
                    state = new DieState(player);
                    break;
            }

            cachedState.Add(key, state);
            return state;
        }
    }
}

