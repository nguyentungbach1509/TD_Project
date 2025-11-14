using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using SubScripts;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.StateMachine
{
    public class PlayerState : State
    {
        protected PlayerController player;
        protected Dictionary<string, InputAction> input;

        public PlayerStateController State => player.State;

        public PlayerState(CharacterBase character) : base(character)
        {
            player = character as PlayerController;
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

