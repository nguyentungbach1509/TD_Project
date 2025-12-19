using Game.Scripts.UI.HUD;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    [CreateAssetMenu(fileName = "SkillSlotData", menuName = "Data/Skills/Slots/SkillSlotData")]
    public class SkillSlotData : SlotData
    {
        [SerializeField] private SkillData skillData;

        public Skill GetSkill() => new Skill(skillData);
    }
}


