using Game.Scripts.Player.Controller;
using SubScripts.Singleton;
using System;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    public class SkillManager : SingletonBase<SkillManager>
    {
        private Vector3Int lastPos;
        private Skill currentSkill;
        private bool isOnHover;
        private PlayerInputController inputCtrl => PlayerInputController.Instance;

        #region Skill LifeCircle

        public void Init()
        {
            inputCtrl.OnMouseLeftClick -= UseSkill;
            inputCtrl.OnMouseLeftClick += UseSkill;

            lastPos = inputCtrl.GridMousePos();
        }

        public void UpdateSkill()
        {
            if (!isOnHover || currentSkill == null) return;
            if (lastPos == inputCtrl.GridMousePos()) return;
            currentSkill.ShowAoe(inputCtrl.GridMousePos());
            lastPos = inputCtrl.GridMousePos();
        }
        #endregion

        #region Skill Action

        public void SelectSkill(Skill skill, Action<float> OnCooldownChange)
        {
            if (skill.OnCooldown) return;

            StartCoroutine(skill.CooldownSkill(OnCooldownChange));

            if(skill.TypeTarget == ESkillTarget.None)
            {
                skill.UseActive();
                return;
            }

            isOnHover = true;
            currentSkill = skill;
        }

        private void UseSkill(Vector3Int pos)
        {
            if (!isOnHover || currentSkill == null) return;
            currentSkill.UseActive();
            isOnHover = false;
            currentSkill = null;
        }
       
        #endregion
    }
}
