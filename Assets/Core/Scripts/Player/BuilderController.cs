using Game.Scripts.StatsCharacter;
using Game.Scripts.Player.StateMachine;


namespace Game.Scripts.Player.Controller
{
    public class BuilderController : CharacterBase
    {
        
        public override void UpdateCharacter()
        {
            if (!isInit) return;
            stateController.UpdateState();
        }

    }
}

