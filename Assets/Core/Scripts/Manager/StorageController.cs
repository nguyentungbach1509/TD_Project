using System;
using UnityEngine;
namespace Game.Scripts.Manager
{
    public static class StorageController
    {
        private static int golds;
        private static int lumbers;
        private static int foods;

        public static Action<int> OnGoldChange;
        public static Action<int> OnLumberChange;
        public static Action<int> OnFoodChange;

        public static int Golds => golds;
        public static int Lumbers => lumbers;
        public static int Foods => foods;

        public static void AddGolds(int gold)
        {
            golds += gold;
            OnGoldChange?.Invoke(golds);
        }

        public static void UseGolds(int gold)
        {
            golds = Mathf.Clamp(golds - gold, 0, golds);
            OnGoldChange?.Invoke(golds);
        }

        public static void AddLumbers(int lumber)
        {
            lumbers += lumber;
            OnLumberChange?.Invoke(lumbers);
        }

        public static void UseLumbers(int lumber)
        {
            lumbers = Mathf.Clamp(lumbers - lumber, 0, lumbers);
            OnLumberChange?.Invoke(lumbers);
        }

        public static void AddFoods(int food)
        {
            foods += food;  
            OnFoodChange?.Invoke(food);
        }

        public static void UseFoods(int food)
        {
            foods = Mathf.Clamp(food - food, 0, foods);
            OnFoodChange?.Invoke(food);
        }
    }

}
