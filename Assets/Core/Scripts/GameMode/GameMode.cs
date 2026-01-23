using Game.Scripts.BuildingLogic;
using Game.Scripts.Manager;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Player.Controller;
using Game.Scripts.SkillMechanic;
using Game.Scripts.StatsCharacter;
using Game.Scripts.UI;
using Subscripts.Spawn;
using SubScripts.Singleton;
using UnityEngine;


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
        protected BuildManager buildManager => BuildManager.Instance;
        protected PlayerInputController playerInputController => PlayerInputController.Instance;
        protected GridManager gridManager => GridManager.Instance;
        protected CameraController cameraController => CameraController.Instance;
        protected SpawnManager spawnManager => SpawnManager.Instance;
        protected UIManager uiManger => UIManager.Instance;
        protected SkillManager skillManager => SkillManager.Instance;

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
            Building building = buildManager.GetBuilding(position);

            selectedUnit = null;

            if (building == null && character == null)
            {
                uiManger.HideHUD();
                return;
            }

            uiManger.ShowHUD();

            if(character != null)
            {
                if (character.CharacterStats.Side == ECharacterSide.Ally)
                {
                    UnitController.ClearAllSelectedDetection();
                    selectedUnit = character;
                    selectedUnit.HUD.ShowSelectedDetection();
                    playerInputController.OnMouseRightClick -= selectedUnit.State.SetTarget;
                    playerInputController.OnMouseRightClick += selectedUnit.State.SetTarget;
                }

                uiManger.ChangeHUDOnSelect(character);
                return;
            }
            uiManger.ChangeHUDOnSelect(building);
        }
    }
}

