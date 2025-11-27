using Game.Scripts.BuildingLogic.Data;
using UnityEngine;

namespace Game.Scripts.BuildingLogic
{
    [CreateAssetMenu(fileName = "BuildModelData", menuName = "Data/Buildings/Model")]
    public class BuildingModelData : ScriptableObject
    {
        [SerializeField] int id;
        [SerializeField] EBuildingType type;
        [SerializeField] BuildingModel prefab;

        public int Id => id;
        public EBuildingType Type => type;
        public BuildingModel Prefab => prefab;
    }
}

