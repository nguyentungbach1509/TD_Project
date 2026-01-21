using Game.Scripts.GamePlay;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.StatsCharacter;
using SubScripts;
using SubScripts.Pooling;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Projectiles
{
    public class Projectile : PoolableComponent
    {
        [SerializeField, ReadOnly] protected string key;
        [SerializeField] protected float damage;
        [SerializeField] protected float speed;
        [SerializeField] protected AnimationController anim;

        protected Vector3Int target;
        protected CharacterBase character;
        protected GridManager grid => GridManager.Instance;
        protected Action<Projectile> onProjectileDestroy;
        protected Coroutine flyCoroutine;

        public string Key => key;

        public virtual void Init(string key, Action<Projectile> _func)
        {
            this.key = key;
            onProjectileDestroy -= _func;
            onProjectileDestroy += _func;
        }

        public virtual void Fire(CharacterBase source, Vector3 target)
        {
            character = source;
            this.target = grid.WorldToGrid(target);
            
            if(flyCoroutine != null) StopCoroutine(flyCoroutine);
            flyCoroutine = StartCoroutine(FlyProgress(target));
            
        }

        protected virtual IEnumerator FlyProgress(Vector3 target)
        {

            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target,
                    speed * Time.deltaTime
                );
                CheckImpact();
                yield return null;
            }

            transform.position = target; // đảm bảo khớp chính xác
        }

        protected virtual void CheckImpact()
        {
            Vector3Int currentIntPos = grid.WorldToGrid(transform.position);
            CharacterBase characterBase = UnitController.GetCharacter(currentIntPos);
            if (characterBase == null) return;
            if (characterBase == character) return;
            characterBase.Stats.TakeDamage(damage);

        }

        protected virtual void DestroyProjectile()
        {
            onProjectileDestroy?.Invoke(this);
        }
    }
}

