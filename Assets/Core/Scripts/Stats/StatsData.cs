using UnityEngine;

namespace Game.Scripts.StatsCharacter
{
    public enum ECharacterType
    {
        Choper, Digger, Fixer, Builder, Soldier
    }

    public enum ECharacterSide
    {
        Enemy, Ally
    }

    [CreateAssetMenu(fileName = "StatsData", menuName = "Data/Character/Stats")]
    public class StatsData : ScriptableObject
    {
        [Header("General")]
        [SerializeField] float maxHp;
        [SerializeField] float moveSpeed;
        [SerializeField] float damage;
        [SerializeField] float armor;
        [SerializeField] ECharacterType characterType;
        [SerializeField] ECharacterSide characterSide;

        [Header("Builder")]
        [SerializeField] float fixingDamage;
        [SerializeField] float harvestAmount;
        [SerializeField] float harvestInterval;

        public float MaxHp => maxHp;    
        public float MoveSpeed => moveSpeed;
        public float Damage => damage;
        public float Armor => armor;
        public ECharacterType CharacterType => characterType;
        public ECharacterSide CharacterSide => characterSide;
        
        public float FixingDamage => fixingDamage;
        public float HarvestAmount => harvestAmount;
        public float HarvestInterval => harvestInterval;
    }
}

