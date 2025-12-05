using TMPro;
using DG.Tweening;
using UnityEngine;
using SubScripts.Pooling;
using Game.Scripts.UICustome;
using Game.Scripts.UI;

namespace Game.Scripts.ObstacleResource
{
    public class ResourceObstacleHUD : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] TMP_Text_Custom textPrefab;

        [Header("Configs")]
        [SerializeField] Vector3 offset;
        [SerializeField] float duration;  


        private ObjectPool<TMP_Text_Custom> pool;
        private UIManager UIManager => UIManager.Instance;
        private Canvas resourceCanvas;

        public void Init()
        {
            pool = PoolManager.CreateOrGetPool(textPrefab);
            resourceCanvas = UIManager.ResourceCanvas;
        }

        public void EffectResourceText(int point)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + offset);

            TMP_Text_Custom customTxt = pool.Spawn();
            customTxt.transform.SetParent(resourceCanvas.transform, false);

            RectTransform rect = customTxt.transform as RectTransform;
            rect.position = screenPos;

            customTxt.Text.text = $"+{point}";
            customTxt.CanvasGroup.alpha = 1;

            // Tween bay lên + fade
            Sequence s = DOTween.Sequence();
            s.Append(rect.DOAnchorPosY(rect.anchoredPosition.y + offset.y, duration));
            s.Join(customTxt.CanvasGroup.DOFade(0, duration));
            s.OnComplete(() => pool.Despawn(customTxt));
        }
    }
}

