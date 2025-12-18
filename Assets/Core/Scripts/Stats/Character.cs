using Game.Scripts.BaseScripts.Abstract;
using Game.Scripts.StatsCharacter.WorldUI;
using Game.Scripts.UI.HUD;
using SubScripts;
using System;
using UnityEngine;

namespace Game.Scripts.StatsCharacter
{
    public class Character : Stats
    {
        private StatsData data;

        protected float moveSpeed;
        protected ECharacterType characterType;
        protected ECharacterSide characterSide;

        protected float fixingDmg;
        protected float harvestAmount;
        protected float harvestInterval;


        public float MoveSpeed => moveSpeed;
        public ECharacterSide Side => characterSide;
        public ECharacterType Type => characterType;


        public float FixingDmg => fixingDmg;
        public float HarvestAmount => harvestAmount;
        public float HarvestInterval => harvestInterval;

       
        public Character(StatsData stats, CharacterHUD hud)
        {
            data = stats;
            key = stats.Key;
            name = stats.Name;
            avatar = stats.Avatar;
            maxhp = stats.MaxHp;
            hp = maxhp;
            moveSpeed = stats.MoveSpeed;
            damage = stats.Damage;
            armor = stats.Armor;
            characterSide = stats.CharacterSide;
            characterType = stats.CharacterType;
            level = 1;
            fixingDmg = stats.FixingDamage;
            harvestAmount = stats.HarvestAmount;
            harvestInterval = stats.HarvestInterval;
            slots = stats.Slots;
            OnTakeDamage -= hud.HpBar.UpdateHpBar;
            OnTakeDamage += hud.HpBar.UpdateHpBar;
        }

        public void TakeDamage(float damage)
        {
            float realDmg = Mathf.Clamp(damage - armor, 0, damage);
            hp = Mathf.Clamp(hp - realDmg, 0, maxhp);
            OnTakeDamage?.Invoke(hp/maxhp);
            OnHealthDetailChange?.Invoke(hp, maxhp);
        }

        public void TakeDamage(DamageInfor damageInfor)
        {
            float realDmg = Mathf.Clamp(damageInfor.Damage - armor, 0, damageInfor.Damage);
            hp = Mathf.Clamp(hp - realDmg, 0, maxhp);
            OnTakeDamage?.Invoke(hp/maxhp);
            OnHealthDetailChange?.Invoke(hp, maxhp);
        }

        public void ChangeHp(float amount)
        {
            hp = Mathf.Clamp(hp+amount, 0, maxhp);
        }
    }

}
