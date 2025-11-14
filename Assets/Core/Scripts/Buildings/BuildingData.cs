using System;
using UnityEngine;

namespace Game.Scripts.BuidlingLogic.Data
{
    public enum EBuidlingType
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
        [SerializeField] protected string key;
        [SerializeField] protected string buildingName;
        [SerializeField] protected string description;
        [SerializeField] protected EBuidlingType type;
        [SerializeField] protected float maxHp;
        [SerializeField] protected float armor;
        [SerializeField] protected float damage;
        [SerializeField] protected Sprite buildingIcon;
        [SerializeField] protected Sprite buildingModel;
        [SerializeField] protected int golds;
        [SerializeField] protected int lumbers;
        [SerializeField] protected int foods;

        [Header("Update Requirements")]
        [SerializeField] protected UpdateRequirement[] requirements;

        [Header("Prefab")]
        [SerializeField] protected Building prefab;

        public string Key => key;   
        public string BuildingName => buildingName;
        public string Description => description;


        public EBuidlingType Type => type;
        public float MaxHp => maxHp;
        public float Armor => armor;
        public float Damage => damage;  
        public Sprite Icon => buildingIcon;
        public Sprite Model => buildingModel;
        
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
        public Sprite UpdateModel;
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
        public EBuidlingType RequiredBuildings;
        public int LevelBuilding;
    }
}


