using Game.Scripts.StatsCharacter;
using UnityEngine;
namespace Game.Scripts.Player
{
    [CreateAssetMenu(fileName = "UnitDataPrefab", menuName = "Spawner/Units/Prefab")]
    public class UnitDataPrefab : ScriptableObject
    {
        [SerializeField] string key;
        [SerializeField] CharacterBase prefab;
        [SerializeField] ECharacterSide characterSide;

        public string Key => key;
        public CharacterBase Prefab => prefab;
        public ECharacterSide Side => characterSide;
    }
}

