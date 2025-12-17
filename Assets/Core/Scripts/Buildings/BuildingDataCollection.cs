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

        public  List<BuildingData> GetAllData() 
        {
            List<BuildingData> data = new();
            for(int i = 0; i < dataGroups.Count; i++)
            {
                for(int j = 0; j < dataGroups[i].BuildingData.Count; j++)
                {
                    data.Add(dataGroups[i].BuildingData[j]);
                }
            }

            return data;
        }
    }
}

