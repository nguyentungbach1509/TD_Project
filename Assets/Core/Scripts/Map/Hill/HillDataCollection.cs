using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Map.Hills
{
    [CreateAssetMenu(fileName = "HillDataCollection", menuName = "Data/Map/HillDataCollection")]
    public class HillDataCollection : ScriptableObject
    {
        [SerializeField] List<HillData> data;

        public HillData GetData()
        {
            return data[Random.Range(0, data.Count)];   
        }
    }
}

