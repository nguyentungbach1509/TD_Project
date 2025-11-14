using SubScript.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player.Controller
{
    public class PlayerInputController : SingletonBase<PlayerInputController>
    {
        [SerializeField] InputActionAsset inputActions;
        private Dictionary<string, InputAction> actions = new();
        public Dictionary<string, InputAction> Input => actions;

        public void Init()
        {
            var playerMap = inputActions.FindActionMap("Player");

            foreach (var action in playerMap.actions)
            {
                actions[action.name] = action;
                action.Enable();
                LoadBinding(action); // load key đã custom (nếu có)
            }
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

    }
}

