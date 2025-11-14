using Game.Scripts.Map.Mechanic;
using Game.Scripts.Player.Controller;
using SubScript.Singleton;
using UnityEngine;

namespace Game.Scripts.Manager
{
    public class GameManager : SingletonBase<GameManager>
    {
        [SerializeField] MapGenerator mapGenerator;
        [SerializeField] PlayerController player;
        private GridManager gridManager => GridManager.Instance;
        private CameraController cameraController => CameraController.Instance;
        private PlayerInputController playerInput => PlayerInputController.Instance;

        private void Start()
        {
            playerInput.Init();
            gridManager.Init();
            mapGenerator.Init();
            player.Init();
            cameraController.Init(player);
        }

        private void Update()
        {
            player.UpdateController();
        }

        private void LateUpdate()
        {
            cameraController.FollowPlayer();
        }
    }
}


