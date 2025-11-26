using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuildingLogic.Data
{
    [CreateAssetMenu(fileName = "BuildingDataGroup", menuName = "Data/Buildings/Group")]
    public class BuildingDataGroup : ScriptableObject
    {
        [SerializeField] private EBuidlingType group;
        [SerializeField] private List<BuildingData> buildingData;

        public EBuidlingType Group => group;

        public Building GetBuilding(string key)
        {
            return buildingData.Find(x => x.Key == key).Prefab;    
        }

        public BuildingData GetBuildingData(string key)
        {
            return buildingData.Find(x => x.Key == key);
        }
    }
}

