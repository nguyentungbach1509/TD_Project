using Game.Scripts.Manager;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Player.Controller;
using Game.Scripts.StatsCharacter;
using Subscripts.Spawn;
using SubScripts.Singleton;
using System.Collections.Generic;
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
        [SerializeField] protected CharacterBase selectedUnit;

        protected PlayerInputController playerInputController => PlayerInputController.Instance;
        protected GridManager gridManager => GridManager.Instance;
        protected CameraController cameraController => CameraController.Instance;
        protected SpawnManager spawnManager => SpawnManager.Instance;

        public CharacterBase SelectedUnit => selectedUnit;

        public virtual void Init()
        {
            playerInputController.OnMouseLeftClick -= Selected;
            playerInputController.OnMouseLeftClick += Selected;
            UnitController.Init();
        }
        public abstract void UpdateGame();
        public abstract void StartGame();

        public abstract void LateUpdateGame();

        public void Selected(Vector3Int position)
        {
            CharacterBase character = UnitController.GetCharacter(position);
            if (character == null) return;
            if (character.Stats.Side == ECharacterSide.Ally)
            {
                UnitController.ClearAllSelectedDetection();
                selectedUnit = character;
                selectedUnit.HUD.ShowSelectedDetection();
                playerInputController.OnMouseRightClick -= selectedUnit.State.SetTarget;
                playerInputController.OnMouseRightClick += selectedUnit.State.SetTarget;
            }
            return;
        }
    }
}

