using Game.Scripts.StatsCharacter;
using Game.Scripts.UI.HUD;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    public abstract class SkillSlotData : SlotData
    {
        [SerializeField] protected SkillData skillData;

        public abstract Skill GetSkill(CharacterBase character);
    }
}


