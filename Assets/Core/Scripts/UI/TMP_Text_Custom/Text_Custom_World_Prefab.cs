using Game.Scripts.UICustom;
using UnityEngine;
namespace Game.Scripts.UI.TextCustom
{
    [CreateAssetMenu(fileName = "Text_Custom_World_Prefab", menuName = "Spawner/Text_Custom/Text_Custom_World_Prefab")]
    public class Text_Custom_World_Prefab : ScriptableObject
    {
        [SerializeField] string key;
        [SerializeField] TMP_Text_Custom prefab;

        public string Key => key;
        public TMP_Text_Custom Prefab => prefab;
    }
}

