using Game.Scripts.StatsCharacter;
using Game.Scripts.Player.StateMachine;
using UnityEngine;
using SubScripts;

namespace Game.Scripts.Player.Controller
{
    public class PlayerController : CharacterBase
    {
        private PlayerStateController stateController;
        public PlayerStateController State => stateController;

        public override void Init()
        {
            base.Init();
            stateController = new PlayerStateController(this);
            isInit = true;
        }

        public override void UpdateCharacter()
        {
            if (!isInit) return;
            stateController.UpdateState();
        }

    }
}

