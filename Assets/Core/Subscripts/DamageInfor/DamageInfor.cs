using Game.Scripts.StatsCharacter;
using System;
using UnityEngine;

namespace SubScripts
{
    public class DamageInfor
    {
        private string id;
        private float damage;
        private Character source;

        public float Damage => damage;
        public Character Source => source;

        public DamageInfor(Character character, float dmg)
        {
            id = Guid.NewGuid().ToString();
            source = character;
            damage = dmg;
        }
    }

}

