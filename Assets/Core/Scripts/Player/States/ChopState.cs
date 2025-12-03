using Game.Scripts.StatsCharacter;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class ChopState : PlayerState
    {
        public ChopState(CharacterBase character) : base(character)
        {
        }

        public override void Enter()
        {
            Debug.Log("Chop State");
            anim.PlayAnimation(AnimationKey.Chop);
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {

        }
    }
}
