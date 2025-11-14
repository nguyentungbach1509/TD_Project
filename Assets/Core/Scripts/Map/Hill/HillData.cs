using UnityEngine;

namespace Game.Scripts.Map.Hills
{
    [CreateAssetMenu(fileName = "HillSO", menuName = "Data/Map/HillSO")]
    public class HillData : ScriptableObject
    {
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] RuleTile ruleTile;

        public int Width => width;
        public int Height => height;
        public RuleTile RuleTile => ruleTile;
    }
}


