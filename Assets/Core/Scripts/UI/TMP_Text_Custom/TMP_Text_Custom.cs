using DG.Tweening;
using SubScripts.Pooling;
using TMPro;
using UnityEngine;
namespace Game.Scripts.UICustom
{
    public class TMP_Text_Custom : PoolableComponent
    {
        [Header("References")]
        [SerializeField] TMP_Text text;
        
        public TMP_Text Text => text;

        public override void OnDespawn()
        {
            base.OnDespawn();
            ResetState();
        }

        private void ResetState()
        {
            // Kill tất cả tween liên quan đến object này
            DOTween.Kill(Text);
            DOTween.Kill(transform);

            // Reset alpha về 1
            if (Text != null)
            {
                Color c = Text.color;
                c.a = 1f;
                Text.color = c;
            }

            // Reset scale/rotation nếu bạn có tween
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
    }
}

