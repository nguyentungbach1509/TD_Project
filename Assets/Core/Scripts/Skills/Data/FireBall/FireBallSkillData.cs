using Game.Scripts.Map.Mechanic;
using Game.Scripts.Projectiles;
using Game.Scripts.StatsCharacter;
using UnityEngine;

namespace Game.Scripts.SkillMechanic
{
    [CreateAssetMenu(fileName = "FireBallSlotData", menuName = "Data/Skills/Slots/FireBall")]
    public class FireBallSkillData : SkillSlotData
    {
        public override Skill GetSkill(CharacterBase character)
        {
            return new FireBallSkill(character, skillData);
        }
    }

    public class FireBallSkill : Skill
    {
        private GridManager grid => GridManager.Instance;

        public FireBallSkill(CharacterBase character, SkillData skillData) : base(character, skillData)
        {

        }

        public override void UseActive()
        {
            if (OnCooldown) return;

            ClearAoe();

            Projectile projectile = spawner.ProjectileSpawner.SpawnProjectile(projectileKey, owner.FirePoint.position, Quaternion.identity);
            foreach (var pos in skillHover.TrackPos)
            {
                projectile.Fire(owner, grid.GridToWorld(pos));
            }
        }
    }
}

