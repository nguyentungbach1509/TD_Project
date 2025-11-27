using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.BuildingLogic.WorldUI;
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Scripts.BuildingLogic
{
    public class BuildingStats
    {
        private string key;
        private string name;
        private string description;
        private BuildingTileSize tileSize;
        private int level;

        private EBuildingType type;
        private float maxHp;
        private float health;
        private float armor;
        private float damage;
        private Sprite buildingIcon;
        private Sprite buildingModel;
        private int golds;
        private int lumbers;
        private int foods;

        private UpdateRequirement[] requirements;

        
        public string Key => key;
        public string BuildingName => name;
        public string Description => description;
        public BuildingTileSize Size => tileSize;
        public int Level => level;  

        public EBuildingType Type => type;
        public float MaxHp => maxHp;
        public float Health => health;
        public float Armor => armor;
        public float Damage => damage;
        public Sprite Icon => buildingIcon;
        public Sprite Model => buildingModel;

        public int Golds => golds;
        public int Lumbers => lumbers;
        public int Foods => foods;
        public UpdateRequirement[] Requirements => requirements;

        public Action<float> OnHpChange;

        public BuildingStats(BuildingData data, BuildingHUD canvas)
        {
            key = data.Key;
            name = data.BuildingName;
            description = data.Description;
            tileSize = data.Size;
            type = data.Type;
            maxHp = data.MaxHp;
            health = data.MaxHp;
            armor = data.Armor;
            damage = data.Damage;
            requirements = data.Requirements;
            level = 1;
            OnHpChange -= canvas.HpBar.UpdateHpBar;
            OnHpChange += canvas.HpBar.UpdateHpBar;
        }

        public void Upgrade(UpdateRequirement req)
        {
            maxHp += req.MultiHp;
            health = maxHp;
            OnHpChange?.Invoke(1);
            damage += req.MultiDmg;
            armor += req.MultiArmor;
            level++;
        }

    }
}

