using Game.Scripts.BaseScripts.Abstract;
using Game.Scripts.BaseScripts.Interface;
using Game.Scripts.Map.Mechanic;
using Game.Scripts.Map.Obstacles;
using Game.Scripts.Player.StateMachine;
using Game.Scripts.StatsCharacter.WorldUI;
using Game.Scripts.UI.HUD;
using SubScripts;
using SubScripts.Pooling;
using UnityEngine;

namespace Game.Scripts.StatsCharacter
{
    public abstract class CharacterBase : PoolableComponent, IBaseGameObject
    {
        [Header("References")]
        [SerializeField] protected StatsData data;
        [SerializeField] protected CharacterHUD hud;
        [SerializeField] protected AnimationController anim;
        
        protected GridManager grid => GridManager.Instance;

        protected StateController stateController;

        protected Character stats;
        protected bool isInit;
        protected Obstacle targetObstacle;
        protected float saveSide;
        public StateController State => stateController;


        public AnimationController Anim => anim;
        public Stats Stats => stats;
        public Character CharacterStats => stats as Character;
        public bool IsInit => isInit;

        public virtual void Init()
        {
            hud.Init();
            stats = new Character(data, hud);
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


        public SlotCollection Slots => stats.Slots;
    }
}

