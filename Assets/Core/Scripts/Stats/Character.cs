using Game.Scripts.StatsCharacter.WorldUI;
using SubScripts;
using System;
using UnityEngine;

namespace Game.Scripts.StatsCharacter
{
    public class Character
    {
        private StatsData data;

        protected float maxHp;
        protected float hp;
        protected float moveSpeed;
        protected float damage;
        protected float armor;
        protected ECharacterType characterType;
        protected ECharacterSide characterSide;

        protected float fixingDmg;
        protected float harvestAmount;
        protected float harvestInterval;

        public float MaxHp => maxHp;
        public float MoveSpeed => moveSpeed;
        public float Damage => damage;
        public float Armor => armor;
        public ECharacterSide Side => characterSide;
        public ECharacterType Type => characterType;


        public float FixingDmg => fixingDmg;
        public float HarvestAmount => harvestAmount;
        public float HarvestInterval => harvestInterval;

        public Action<float> OnTakeDamage;

        public Character(StatsData stats, CharacterHUD hud)
        {
            data = stats;
            maxHp = stats.MaxHp;
            hp = maxHp;
            moveSpeed = stats.MoveSpeed;
            damage = stats.Damage;
            armor = stats.Armor;
            characterSide = stats.CharacterSide;
            characterType = stats.CharacterType;

            fixingDmg = stats.FixingDamage;
            harvestAmount = stats.HarvestAmount;
            harvestInterval = stats.HarvestInterval;
            OnTakeDamage -= hud.HpBar.UpdateHpBar;
            OnTakeDamage += hud.HpBar.UpdateHpBar;
        }

        public void TakeDamage(float damage)
        {
            float realDmg = Mathf.Clamp(damage - armor, 0, damage);
            hp = Mathf.Clamp(hp - realDmg, 0, maxHp);
            OnTakeDamage(hp/maxHp);
        }

        public void TakeDamage(DamageInfor damageInfor)
        {
            float realDmg = Mathf.Clamp(damageInfor.Damage - armor, 0, damageInfor.Damage);
            hp = Mathf.Clamp(hp - realDmg, 0, maxHp);
            OnTakeDamage(hp/maxHp);
        }

        public void ChangeHp(float amount)
        {
            hp = Mathf.Clamp(hp+amount, 0, maxHp);
        }
    }

}
