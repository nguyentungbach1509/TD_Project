using SubScripts.Pooling;
using TMPro;
using UnityEngine;
namespace Game.Scripts.UICustome
{
    public class TMP_Text_Custom : PoolableComponent
    {
        [Header("References")]
        [SerializeField] TMP_Text text;
        [SerializeField] CanvasGroup canvasGroup;
        
        public TMP_Text Text => text;
        public CanvasGroup CanvasGroup => canvasGroup;
        
    }
}

