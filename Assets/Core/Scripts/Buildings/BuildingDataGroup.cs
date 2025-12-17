using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuildingLogic.Data
{
    [CreateAssetMenu(fileName = "BuildingDataGroup", menuName = "Data/Buildings/Group")]
    public class BuildingDataGroup : ScriptableObject
    {
        [SerializeField] private EBuildingType group;
        [SerializeField] private List<BuildingData> buildingData;

        public EBuildingType Group => group;

        public Building GetBuilding(string key)
        {
            return buildingData.Find(x => x.Key == key).Prefab;    
        }

        public BuildingData GetBuildingData(string key)
        {
            return buildingData.Find(x => x.Key == key);
        }

        public List<BuildingData> BuildingData => buildingData;
    }
}

