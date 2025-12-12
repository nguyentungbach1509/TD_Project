using TMPro;
using DG.Tweening;
using UnityEngine;
using SubScripts.Pooling;
using Game.Scripts.UICustom;
using Subscripts.Spawn;
using Subscripts;


namespace Game.Scripts.ObstacleResource
{
    public class ResourceObstacleHUD : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] Vector3 offset;
        [SerializeField] float duration;  

        private SpawnManager spawner => SpawnManager.Instance;

        public void EffectResourceText(int point)
        {
            TMP_Text_Custom customTxt = spawner.ResourceTxtSpawner.SpawnCustomText(Constants.ResourceTxt, transform.position + offset);

            customTxt.Text.text = $"+{point}";
            Vector3 pos = customTxt.transform.position;

            // fade + move up in world space
            DOTween.Sequence()
                .Append(customTxt.transform.DOMoveY(pos.y + 0.75f, 1f)) // bay lên 0.5 tile
                .Join(customTxt.Text.DOFade(0f, 1f))
                .OnComplete(() => spawner.ResourceTxtSpawner.DespawnCustomText(Constants.ResourceTxt, customTxt));
        }

    }
}

