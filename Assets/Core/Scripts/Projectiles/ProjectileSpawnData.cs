using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Projectiles
{
    [CreateAssetMenu(fileName = "ProjectileDataCollection", menuName = "Spawner/Projectile")]
    public class ProjectileSpawnData : ScriptableObject 
    {
        [SerializeField] List<ProjectileData> prefabs;
        public List<ProjectileData> Prefabs => prefabs;
    }

    [System.Serializable]
    public class ProjectileData
    {
        [SerializeField] string key;
        [SerializeField] Projectile prefab;

        public string Key => key;
        public Projectile Prefab => prefab;
    }
}

