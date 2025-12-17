using Game.Scripts.BuildingLogic.Data;
using System.Collections.Generic;

namespace Game.Scripts.BuildingLogic
{
    public static class BuildingController
    {
        private static List<BuildingData> listData;

        public static void Init(GamePrefabs gamePrefabs)
        {
            listData ??= new List<BuildingData>();
            listData.Clear();
            List<BuildingData> tempData = gamePrefabs.BuildingPrefabs.GetAllData();
            for (int i = 0; i < tempData.Count; i++)
            {
                listData.Add(tempData[i]);
            }
        }

        public static List<BuildingData> GetAllData => listData;
        public static BuildingData GetDataAt(int index)
        {
            return listData[index]; 
        }
        public static int DataCount => listData.Count;
    }
}

