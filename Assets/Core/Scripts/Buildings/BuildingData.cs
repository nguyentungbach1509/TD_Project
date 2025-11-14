using System;
using UnityEngine;

namespace Game.Scripts.BuidlingLogic
{
    public enum EBuidlingType
    {
        Basement,
        Tower,
        Farm,
        Statue,
        Wall,
        Smith,
        Units
    }

    public abstract class BuildingData : ScriptableObject
    {
        [SerializeField] protected string key;
        [SerializeField] protected EBuidlingType type;
        [SerializeField] protected float health;
        [SerializeField] protected float armor;
        [SerializeField] protected float damage;
        [SerializeField] protected Sprite buildingIcon;
        [SerializeField] protected int golds;
        [SerializeField] protected int lumbers;
        [SerializeField] protected int foods;


    }

    [Serializable]
    public class UpdateRequirement
    {
        public int TargetLevel;
        public EBuidlingType RequiredBuilding;
        public float MultiHp;
        public float MultiArmor;
        public float MultiDmg;

        public int RequiredGolds;
        public int RequiredLumbers;
        public int RequiredFood;
    }
}


