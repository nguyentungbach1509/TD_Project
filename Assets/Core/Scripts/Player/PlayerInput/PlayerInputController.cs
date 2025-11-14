using Game.Scripts.Map.Mechanic;
using Subscripts;
using SubScripts.Singleton;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.Controller
{
    public class PlayerInputController : SingletonBase<PlayerInputController>
    {
        [SerializeField] InputActionAsset inputActions;
        
        private GridManager gridManager => GridManager.Instance;
        private Dictionary<string, InputAction> actions = new();
        private Camera mainCamera => Camera.main;

        public Dictionary<string, InputAction> Input => actions;

        public Action<Vector3Int> OnMouseClick;
        public Action OnMouseUp;


        public void Init()
        {
            var playerMap = inputActions.FindActionMap("Player");

            foreach (var action in playerMap.actions)
            {
                actions[action.name] = action;
                action.Enable();
                LoadBinding(action); // load key đã custom (nếu có)
            }

            actions[Constants.Left_Click].started += MouseInput_OnClick;
            actions[Constants.Left_Click].canceled += MouseInput_OnUp;
        }

        public void StartRebind(string actionName, int bindingIndex)
        {
            var action = actions[actionName];
            action.Disable();

            action.PerformInteractiveRebinding(bindingIndex)
                .WithControlsExcluding("<Mouse>/position") // loại trừ input không mong muốn
                .OnComplete(op =>
                {
                    op.Dispose();
                    action.Enable();
                    SaveBinding(action, bindingIndex);
                    Debug.Log($"New binding for {actionName}: {action.bindings[bindingIndex].effectivePath}");
                })
                .Start();
        }

        void SaveBinding(InputAction action, int bindingIndex)
        {
            string key = action.actionMap + "/" + action.name + "/" + bindingIndex;
            PlayerPrefs.SetString(key, action.bindings[bindingIndex].overridePath);
        }

        void LoadBinding(InputAction action)
        {
            for (int i = 0; i < action.bindings.Count; i++)
            {
                string key = action.actionMap + "/" + action.name + "/" + i;
                if (PlayerPrefs.HasKey(key))
                {
                    string overridePath = PlayerPrefs.GetString(key);
                    if (!string.IsNullOrEmpty(overridePath))
                        action.ApplyBindingOverride(i, overridePath);
                }
            }
        }
        #region Mouse Input
        private void MouseInput_OnClick(InputAction.CallbackContext ctx)
        {
            Vector2 mousePos = actions[Constants.Mouse_Hover].ReadValue<Vector2>();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;
            OnMouseClick?.Invoke(gridManager.WorldToGrid(worldPos));
        }

        private void MouseInput_OnUp(InputAction.CallbackContext ctx)
        {
            OnMouseUp?.Invoke();
        }

        public Vector3Int GridMousePos()
        {
            Vector2 mousePos = actions[Constants.Mouse_Hover].ReadValue<Vector2>();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.z = 0;
            return gridManager.WorldToGrid(worldPos);
        }

        #endregion
    }
}

