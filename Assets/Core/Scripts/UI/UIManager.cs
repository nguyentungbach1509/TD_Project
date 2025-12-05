using SubScripts.Singleton;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class UIManager : SingletonBase<UIManager>
    {
        [SerializeField] Canvas resourceCanvas;

        public Canvas ResourceCanvas => resourceCanvas;
    }
}

