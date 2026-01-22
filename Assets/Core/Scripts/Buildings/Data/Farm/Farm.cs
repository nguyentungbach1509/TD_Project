using Game.Scripts.Manager;
using UnityEngine;

namespace Game.Scripts.BuildingLogic
{
    public class Farm : Building
    {
        [SerializeField] int foodProducts;
        protected override void StartBuild(Vector3Int pos)
        {
            base.StartBuild(pos);
            CollectedResourcesController.AddFoods(foodProducts);
        }
    }
}

