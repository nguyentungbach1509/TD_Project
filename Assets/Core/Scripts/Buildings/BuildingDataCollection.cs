using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.BuidlingLogic.Data
{
    [CreateAssetMenu(fileName = "BuildingDataCollection", menuName = "Data/Buildings/Collection")]
    public class BuildingDataCollection : ScriptableObject
    {
        [SerializeField] List<BuildingDataGroup> dataGroups;

        public BuildingData GetData(EBuidlingType group, string key)
        {
            BuildingDataGroup groupData = dataGroups.Find(x => x.Group == group);
            return groupData.GetBuildingData(key);
        }

        public Building GetBuilding(EBuidlingType group, string key)
        {
            BuildingDataGroup groupData = dataGroups.Find(x => x.Group == group);
            return groupData.GetBuilding(key);
        }
    }
}

