using System;
using UnityEngine;

namespace Game.Scripts.BuildingLogic.Data
{
    public enum EBuildingType
    {
        Basement,

        //Towers
        MagicTower,
        PhysicTower,
        UltraTower,

        Farm,
        Statue,
        Wall,
        Smith,
        FarmUnits,
        ArmyUnits
    }

    [CreateAssetMenu(menuName = "Data/Buildings/Stats")]
    public class BuildingData : ScriptableObject
    {
        [Header("Base data")]
        [SerializeField] protected string key;
        [SerializeField] protected string buildingName;
        [SerializeField] protected string description;
        [SerializeField] protected EBuildingType type;
        [SerializeField] protected BuildingTileSize size;

        [Header("Stats Data")]
        [SerializeField] protected float maxHp;
        [SerializeField] protected float armor;
        [SerializeField] protected float damage;

        [Header("Appearance Data")]
        [SerializeField] protected Sprite buildingIcon;
        

        [Header("Cost Data")]
        [SerializeField] protected int golds;
        [SerializeField] protected int lumbers;
        [SerializeField] protected int foods;

        [Header("Update Requirements Data")]
        [SerializeField] protected UpdateRequirement[] requirements;

        [Header("Prefab Data")]
        [SerializeField] protected Building prefab;

        public string Key => key;   
        public string BuildingName => buildingName;
        public string Description => description;
        public BuildingTileSize Size => size;

        public EBuildingType Type => type;
        public float MaxHp => maxHp;
        public float Armor => armor;
        public float Damage => damage;  
        public Sprite Icon => buildingIcon;
        
        public int Golds => golds;
        public int Lumbers => lumbers;
        public int Foods => foods;
        public UpdateRequirement[] Requirements => requirements;
        public Building Prefab => prefab;    
    }

    [Serializable]
    public class UpdateRequirement
    {
        public int TargetLevel;
        public Sprite UpdateIcon;
        public BuildingModelData UpdateModel;
        public RequiredBuilding[] RequiredBuildings;

        public float MultiHp;
        public float MultiArmor;
        public float MultiDmg;

        public int RequiredGolds;
        public int RequiredLumbers;
        public int RequiredFoods;
    }

    [Serializable]
    public class RequiredBuilding
    {
        public EBuildingType RequiredBuildings;
        public int LevelBuilding;
    }

    [Serializable] 
    public class BuildingTileSize
    {
        public int Width;
        public int Height;
    }
}


