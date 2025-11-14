using Game.Scripts.Map.Mechanic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.TileController.Data
{
    [CreateAssetMenu(fileName = "TileData", menuName = "Data/Tile/TileData")]
    public class TileDataSO : ScriptableObject
    {

    }

    [Serializable]
    public class TileData
    {
        public ETile Type;
        public RuleTile Tile;
    }

}


