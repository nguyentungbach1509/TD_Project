using SubScripts;
using UnityEngine;
namespace Game.Scripts.BuildingLogic
{
    public class BuildingModel : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] private AnimationController anim;
        [SerializeField] private SpriteRenderer modelSprite;

        private Color save_color_to_test;

        public void BlurSprite()
        {
            Color clr = modelSprite.color;
            save_color_to_test = clr;
            clr.a = .8f;
            modelSprite.color = clr;
        }

        public void WarningSprite(bool isBlocked)
        {
            Color clr = isBlocked ? Color.red : save_color_to_test;
            clr.a = .8f;
            modelSprite.color = clr;
        }

        public void FixedModel()
        {
            modelSprite.color = save_color_to_test;
        }
    }
}

