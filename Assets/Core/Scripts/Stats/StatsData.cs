using Game.Scripts.UI.HUD;
using Unity.Collections;
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
        [SerializeField] string key;
        [SerializeField] string charName;
        [SerializeField] Sprite avatar;
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

        [Header("Slot Data")]
        [SerializeField] SlotCollection slots;

        public string Key => key;
        public string Name => charName;
        public Sprite Avatar => avatar;
        public float MaxHp => maxHp;    
        public float MoveSpeed => moveSpeed;
        public float Damage => damage;
        public float Armor => armor;
        public ECharacterType CharacterType => characterType;
        public ECharacterSide CharacterSide => characterSide;
        
        public float FixingDamage => fixingDamage;
        public float HarvestAmount => harvestAmount;
        public float HarvestInterval => harvestInterval;

        public SlotCollection Slots => slots;
    }
}

