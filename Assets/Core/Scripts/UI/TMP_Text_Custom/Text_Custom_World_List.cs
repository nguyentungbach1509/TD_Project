using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.TextCustom
{
    [CreateAssetMenu(fileName = "Text_Custom_World_List", menuName = "Spawner/UI/Text_Custom/Text_Custom_World_List")]
    public class Text_Custom_World_List : ScriptableObject
    {
        [SerializeField] private List<Text_Custom_World_Prefab> prefabs;
        public List<Text_Custom_World_Prefab> Prefabs => prefabs;
    }
}

