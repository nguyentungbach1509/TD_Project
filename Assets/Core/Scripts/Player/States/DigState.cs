using Game.Scripts.Player.StateMachine;
using Game.Scripts.StatsCharacter;
using SubScripts.Constants;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public class DigState : BuilderState
    {
        public DigState(CharacterBase character) : base(character)
        {
        }

        public override void Enter()
        {
            Debug.Log("Dig State");
            anim.PlayAnimation(AnimationKey.Dig);
        }
    }

}
