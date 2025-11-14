using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using SubScripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.StateMachine
{
    public abstract class State
    {
        protected CharacterBase character;
        protected Character stats;
        protected AnimationController anim;

        public State(CharacterBase character)
        {
            this.character = character;
            anim = character.Anim;
            stats = character.Stats;
        }

        public Rigidbody2D Rb => character.Rb;

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Execute();
    }


}
