using Game.Scripts.BaseScripts;
using Game.Scripts.GamePlay;
using Game.Scripts.StatsCharacter;
using Subscripts.Spawn;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    public class Skill 
    {
        protected string skillName;
        protected string skillKey;
        protected ESkill skillType;
        protected ESkillTarget skillTargetType;
        protected ECharacterSide sideTarget;
        protected string skillDescription;
        protected Sprite skillIcon;
        protected int skillRange;
        protected float skillDmg;
        protected float skillCd;
        protected float skillTime;

        protected SpawnManager spawner => SpawnManager.Instance;

        [Header("Skill Hover")]
        protected ESkillHover typeHover;
        protected int skillAoe;
        protected SkillHover skillHover;


        protected SkillData data;
        protected Coroutine cdCoroutine;
        
        public string SkillName => skillName;
        public string SkillKey => skillKey;
        public string SkillDescription => skillDescription;
        public int SkillRange => skillRange;
        public int SkillAoe => skillAoe;
        public Sprite SkillIcon => skillIcon;
        public ESkill SkillType => skillType;
        public ESkillHover TypeHover => typeHover;
        public ESkillTarget TypeTarget => skillTargetType;
        public ECharacterSide SideTarget => sideTarget;    
        public SkillHover SkillHover => skillHover;
        public float SkillDmg => skillDmg;
        public float SkillCd => skillCd;

        public bool OnCooldown { get; set; }


        public Skill(SkillData skillData)
        {
            data = skillData;
            skillName = skillData.name;
            skillKey = skillData.SkillKey;
            skillType = skillData.SkillType;
            skillDescription = skillData.SkillDescription;
            skillIcon = skillData.SkillIcon;
            skillRange = skillData.SkillRange;
            skillDmg = skillData.SkillDmg;
            skillCd = skillData.SkillCd;
            skillTime = skillData.SkillCd;
            typeHover = skillData.TypeHover;
            sideTarget = skillData.SideTarget;
            skillHover = skillData.SkillHover;
            skillAoe = skillData.SkillAoe;
        }

        public virtual void UsePassive()
        {
            ClearAoe();
        }

        public virtual void UseActive()
        {
            if (OnCooldown) return;
            
            ClearAoe();
            
            foreach(var pos in skillHover.TrackPos)
            {
                CharacterBase character = UnitController.GetCharacter(pos);
                if (character == null || character.CharacterStats.Side != sideTarget) continue;
                //Dinh hieu ung va damage cua skill
                character.Stats.TakeDamage(skillDmg);
            }

            skillHover.TrackPos.Clear();
        }

        public virtual IEnumerator CooldownSkill(Action<float> OnCooldownChange)
        {
            OnCooldown = true;
            skillTime = skillCd;

            while (skillTime > 0)
            {
                OnCooldownChange?.Invoke(skillTime/skillCd);
                skillTime -= Time.deltaTime;
                yield return null;
            }

            skillTime = 0;
            OnCooldown = false;
        }

        public void ShowAoe(Vector3Int position) => skillHover.ShowHoverAoe(position, skillAoe);
        public void ClearAoe() => skillHover.ClearHoverAoe();
    }
}

