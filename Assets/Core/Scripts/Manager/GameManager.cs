using Game.Scripts.GamePlay;
using Game.Scripts.Player.Controller;
using SubScripts.Singleton;

namespace Game.Scripts.Manager
{
    public class GameManager : SingletonBase<GameManager>
    {
        private PlayerInputController playerInput => PlayerInputController.Instance;
        private GameMode currentMode;
        private bool isInit;

        private void Start()
        {
            playerInput.Init();
            isInit = true;
        }

        private void Update()
        {
            if (!isInit) return;
            currentMode.UpdateGame();
        }

        private void LateUpdate()
        {
            if (!isInit) return;
            currentMode.LateUpdateGame();
        }

        private void SelectedMode(GameMode mode)
        {
            currentMode = mode;
            currentMode.Init();
        }
    }
}


