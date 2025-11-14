using UnityEngine;

namespace Game.Scripts.StatsCharacter
{
    [CreateAssetMenu(fileName = "StatsData", menuName = "Data/Character/Stats")]
    public class StatsData : ScriptableObject
    {
        [SerializeField] float maxHp;
        [SerializeField] float moveSpeed;
        [SerializeField] float damage;
        [SerializeField] float armor;

        public float MaxHp => maxHp;    
        public float MoveSpeed => moveSpeed;
        public float Damage => damage;
        public float Armor => armor;
    }
}

