using Game.Scripts.UI.HUD;
using SubScripts;
using System;
using UnityEngine;

namespace Game.Scripts.BaseScripts.Abstract
{
    public abstract class Stats
    {
        protected string key;
        protected string name;
        protected Sprite avatar;
        protected float hp;
        protected float maxhp;
        protected float armor;
        protected int level;
        protected float damage;
        protected SlotCollection slots;

        public string Key => key;
        public string Name => name;
        public Sprite Avatar => avatar;
        public float HP => hp;
        public float MaxHP => maxhp;
        public float Armor => armor;
        public int Level => level;
        public float Damage => damage;

        public SlotCollection Slots { get; }

        public Action<float> OnTakeDamage;
        public Action<float, float> OnHealthDetailChange;

        public virtual void TakeDamage(float damage)
        {
            float realDmg = Mathf.Clamp(damage - armor, 0, damage);
            hp = Mathf.Clamp(hp - realDmg, 0, maxhp);
            OnTakeDamage?.Invoke(hp / maxhp);
            OnHealthDetailChange?.Invoke(hp, maxhp);
        }

        public virtual void TakeDamage(DamageInfor damageInfor)
        {
            float realDmg = Mathf.Clamp(damageInfor.Damage - armor, 0, damageInfor.Damage);
            hp = Mathf.Clamp(hp - realDmg, 0, maxhp);
            OnTakeDamage?.Invoke(hp / maxhp);
            OnHealthDetailChange?.Invoke(hp, maxhp);
        }

        public virtual void BuffHealth(float amount)
        {
            hp = Mathf.Clamp(hp + amount, 0, maxhp);
            OnTakeDamage?.Invoke(hp / maxhp);
            OnHealthDetailChange?.Invoke(hp, maxhp);
        }
    }

}
