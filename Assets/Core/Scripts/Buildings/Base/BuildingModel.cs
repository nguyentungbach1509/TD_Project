using SubScripts;
using UnityEngine;
namespace Game.Scripts.BuidlingLogic
{
    public class BuildingModel : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private AnimationController anim;
        [SerializeField] private SpriteRenderer modelSprite;

        public void BlurSprite()
        {
            Color clr = modelSprite.color;
            clr.a = .35f;
            modelSprite.color = clr;
        }

        public void WarningSprite(bool isBlocked)
        {
            modelSprite.color = isBlocked ? Color.red : Color.white;
        }
    }
}

