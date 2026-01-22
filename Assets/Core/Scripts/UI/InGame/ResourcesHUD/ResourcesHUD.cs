using UnityEngine;
namespace Game.Scripts.UI
{
    public class ResourcesHUD : MonoBehaviour
    {
        [SerializeField] ResourcesTabHUD[] tabHUDs;

        public void Init()
        {
            for(int i = 0; i < tabHUDs.Length; i++)
            {
                tabHUDs[i].Init();
            }
        }
    }
}

