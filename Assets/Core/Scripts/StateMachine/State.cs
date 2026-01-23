using Game.Scripts.GamePlay;
using Game.Scripts.Manager;
using Game.Scripts.Path;
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
        protected GameMode gameMode => GameManager.Instance.CurrentMode;
        protected PathFinding pathFinder => PathFinding.Instance;

        public State(CharacterBase character)
        {
            this.character = character;
            anim = character.Anim;
            stats = character.CharacterStats;
        }


        public abstract void Enter();
        
        public abstract void Exit();
        public abstract void Execute();
    }


}
