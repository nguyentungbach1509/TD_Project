
using Game.Scripts.UI.TextCustom;
using UnityEngine;

namespace Game.Scripts.UI
{
    [CreateAssetMenu(fileName = "UIPrefabs", menuName = "Spawner/UI/PrefabCollection")]
    public class UIPrefabs : ScriptableObject
    {
        [Header("Resource Text World")]
        public Text_Custom_World_List ResourceTxts;
    }
}

