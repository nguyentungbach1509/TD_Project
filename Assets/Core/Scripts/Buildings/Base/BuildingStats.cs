using Game.Scripts.BaseScripts.Abstract;
using Game.Scripts.BuildingLogic.Data;
using Game.Scripts.BuildingLogic.WorldUI;
using Game.Scripts.UI.HUD;
using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

namespace Game.Scripts.BuildingLogic
{
    public class BuildingStats : Stats
    {
        private string description;
        private BuildingTileSize tileSize;


        private EBuildingType type;
        private Sprite buildingIcon;
        private Sprite buildingModel;

        private int golds;
        private int lumbers;
        private int foods;
        private float buildTime;
        private EBuildLimit limitBuild;

        private UpdateRequirement[] requirements;
        
        public string BuildingName => name;
        public string Description => description;
        public BuildingTileSize Size => tileSize; 

        public EBuildingType Type => type;

        public Sprite Icon => buildingIcon;
        public Sprite Model => buildingModel;

        public int Golds => golds;
        public int Lumbers => lumbers;
        public int Foods => foods;
        public float BuildTime => buildTime;
        public EBuildLimit Limit => limitBuild;

        public UpdateRequirement[] Requirements => requirements;
        
        public BuildingStats(BuildingData data, BuildingHUD canvas)
        {
            key = data.Key;
            name = data.BuildingName;
            description = data.Description;
            tileSize = data.Size;
            type = data.Type;
            maxhp = data.MaxHp;
            hp = data.MaxHp;
            armor = data.Armor;
            damage = data.Damage;
            buildTime = data.BuildTime;
            limitBuild = data.Limit;
            requirements = data.Requirements;
            level = 1;
            slots = data.Slots;
            OnTakeDamage -= canvas.HpBar.UpdateHpBar;
            OnTakeDamage += canvas.HpBar.UpdateHpBar;
        }


        public void Upgrade(UpdateRequirement req)
        {
            maxhp += req.MultiHp;
            hp = maxhp;
            OnTakeDamage?.Invoke(1);
            damage += req.MultiDmg;
            armor += req.MultiArmor;
            buildTime = req.RequiredBuildTime;
            level++;
        }

    }
}

