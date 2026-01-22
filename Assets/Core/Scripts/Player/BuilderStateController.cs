using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using Game.Scripts.TileController.Mechanic;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class BuilderStateController : StateController
    {
        private BuilderController player;

        public BuilderStateController(CharacterBase character) : base(character)
        {
            player = character as BuilderController;
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
                case AnimationKey.Chop:
                    state = new ChopState(player);      
                    break;
                case AnimationKey.Mining:
                    state = new MiningState(player);
                    break;
                case AnimationKey.Dig:
                    state = new DigState(player);
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

