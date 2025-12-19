using Game.Scripts.BaseScripts;
using Game.Scripts.StatsCharacter;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    public enum ESkill
    {
        Passive,
        Active,
    }

    public enum ESkillTarget
    {
        None,
        Target,
    }

    public enum ESkillHover
    {
        One,
        Square,
        Cross,

    }

    [CreateAssetMenu(fileName = "SkillData", menuName = "Data/Skills/SkillData")]
    public class SkillData : ScriptableObject
    {
        [Header("Base Infor")]
        [SerializeField] string skillName;
        [SerializeField] string skillKey;
        [SerializeField] ESkill skillType;
        [SerializeField] ESkillTarget skillTargetType;
        [SerializeField] ECharacterSide sideTarget;
        [SerializeField] string skillDescription;
        [SerializeField] Sprite skillIcon;
        [SerializeField] int skillRange;
        [SerializeField] float skillDmg;
        [SerializeField] float skillCd;

        [Header("Skill Hover")]
        [SerializeField] ESkillHover typeHover;
        [SerializeField] int skillAoe;
        [SerializeField] SkillHover skillHover;


        public string SkillName => skillName;
        public string SkillKey => skillKey;
        public string SkillDescription => skillDescription;
        public int SkillRange => skillRange;
        public int SkillAoe => skillAoe;
        public Sprite SkillIcon => skillIcon;
        public ESkill SkillType => skillType;
        public ESkillTarget SkillTargetType => skillTargetType;
        public ESkillHover TypeHover => typeHover;
        public SkillHover SkillHover => skillHover;
        public ECharacterSide SideTarget => sideTarget;
        public float SkillDmg => skillDmg;
        public float SkillCd => skillCd;    
    }

}


