using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using SubScripts.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public abstract class StateController
    {
        protected CharacterBase character;
        protected State currentState;
        protected Dictionary<AnimationKey, State> cachedState;

        public StateController(CharacterBase character)
        {
            this.character = character;
            cachedState = new();
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

