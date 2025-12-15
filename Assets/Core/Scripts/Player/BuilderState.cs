using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using SubScripts;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.StateMachine
{
    public class BuilderState : State
    {
        protected BuilderController player;
        protected Dictionary<string, InputAction> input;

        public BuilderStateController State => player.State as BuilderStateController;

        public BuilderState(CharacterBase character) : base(character)
        {
            player = character as BuilderController;
            input = PlayerInputController.Instance.Input;
        }

        public override void Enter()
        {
            
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}

