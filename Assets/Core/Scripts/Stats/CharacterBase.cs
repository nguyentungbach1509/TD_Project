using Game.Scripts.Map.Mechanic;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.StateMachine;
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

        protected StateController stateController;
        private GridManager grid => GridManager.Instance;
        
        protected bool isInit;
        protected Character character;
        protected Obstacle targetObstacle;
        protected float saveSide;
        public StateController State => stateController;

        public AnimationController Anim => anim;
        public Character Stats => character;
        public Rigidbody2D Rb => rb;
        public bool IsInit => isInit;

        public virtual void Init()
        {
            hud.Init();
            character = new Character(stats, hud);
            stateController = new BuilderStateController(this);
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

        public bool InInteractRange()
        {
            if (targetObstacle == null) return false;
            float distance = Vector2.Distance(transform.position, targetObstacle.transform.position);
            return distance <= targetObstacle.InteractRange;
        }

        public abstract void UpdateCharacter();
        
        public Vector3Int GridPos => grid.WorldToGrid(transform.position);
        public CharacterHUD HUD => hud;

        public Obstacle CurrentObstacle
        {
            get => targetObstacle;
            set => targetObstacle = value;
        }

        public Character TargetCharacter
        {
            get => character;
            set => character = value;
        }
    }
}

