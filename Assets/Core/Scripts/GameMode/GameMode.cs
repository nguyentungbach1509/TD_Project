using Game.Scripts.Manager;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Player.Controller;
using SubScripts.Singleton;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.GamePlay
{
    public enum EGameMode
    {
        Survival,
    }

    public abstract class GameMode : SingletonSubclass
    {
        [SerializeField] public EGameMode Mode;
        [SerializeField] protected MapGenerator mapGenerator;
        [SerializeField] protected PlayerController player;
        
        protected GridManager gridManager => GridManager.Instance;
        protected CameraController cameraController => CameraController.Instance;


        public PlayerController Player => player;

        public virtual void Init()
        {
            
        }

        public abstract void UpdateGame();
       

        public abstract void LateUpdateGame();
    }
}

