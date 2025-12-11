using UnityEngine;

namespace Game.Scripts.Map.Hills
{
    [CreateAssetMenu(fileName = "HillSO", menuName = "Data/Map/HillSO")]
    public class HillData : ScriptableObject
    {
        [SerializeField] int width;
        [SerializeField] int height;
        [SerializeField] HillRuleTile ruleTile;

        public int Width => width;
        public int Height => height;
        public HillRuleTile RuleTile => ruleTile;
    }
}


