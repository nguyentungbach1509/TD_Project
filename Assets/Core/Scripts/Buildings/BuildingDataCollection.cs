using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuildingLogic.Data
{
    [CreateAssetMenu(fileName = "BuildingDataCollection", menuName = "Data/Buildings/Collection")]
    public class BuildingDataCollection : ScriptableObject
    {
        [SerializeField] List<BuildingDataGroup> dataGroups;

        public BuildingData GetData(EBuildingType group, string key)
        {
            BuildingDataGroup groupData = dataGroups.Find(x => x.Group == group);
            return groupData.GetBuildingData(key);
        }

        public Building GetBuilding(EBuildingType group, string key)
        {
            BuildingDataGroup groupData = dataGroups.Find(x => x.Group == group);
            return groupData.GetBuilding(key);
        }
    }
}

