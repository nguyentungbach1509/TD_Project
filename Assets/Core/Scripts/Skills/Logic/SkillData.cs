using Game.Scripts.BaseScripts;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    public enum ESkill
    {
        Passive,
        Active,
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
        public ESkillHover TypeHover => typeHover;
        public SkillHover SkillHover => skillHover;
        public float SkillDmg => skillDmg;
        public float SkillCd => skillCd;    
    }

}


