using Game.Scripts.StatsCharacter.WorldUI;
using SubScripts;
using SubScripts.Pooling;
using UnityEngine;

namespace Game.Scripts.StatsCharacter
{
    public abstract class CharacterBase : PoolableComponent
    {
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected StatsData stats;
        [SerializeField] protected CharacterHUD hud;
        [SerializeField] protected AnimationController anim;

        protected bool isInit;
        protected Character character;
        protected float saveSide;

        public AnimationController Anim => anim;
        public Character Stats => character;
        public Rigidbody2D Rb => rb;


        public virtual void Init()
        {
            hud.Init();
            character = new Character(stats, hud);
            isInit = true;
        }

        public void ChangeSide(Vector2 moveVector)
        {
            float x = Mathf.Abs(anim.transform.localScale.x);
            float y = anim.transform.localScale.y;
            
            if (moveVector.x < 0)
            {
                anim.transform.localScale = new Vector3(x * -1f, y, 1);
                saveSide = -1;
                return;
            }

            if(moveVector.x > 0)
            {
                anim.transform.localScale = new Vector3(x, y, 1);
                saveSide = 1;
                return;
            }

            if (saveSide == 0) return;
            anim.transform.localScale = new Vector3(x * saveSide, y, 1);

        }

        public abstract void UpdateCharacter();
        
    }
}

