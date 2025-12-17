using UnityEngine;
namespace Game.Scripts.UI.HUD
{
    [CreateAssetMenu(fileName = "SlotPrefabData", menuName = "Spawner/UI/SlotHUD/SlotPrefabData")]
    public class SlotPrefabData : ScriptableObject
    {
        [SerializeField] string key;
        [SerializeField] SlotHUD prefab;

        public string Key => key;
        public SlotHUD Prefab => prefab;
    }
}

