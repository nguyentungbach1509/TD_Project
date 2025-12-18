using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.UI.HUD
{
    [System.Serializable]
    public class SlotCollection
    {
        public List<SlotData> List = new();

        public void Add(SlotData slot) => List.Add(slot);
        public void Remove(SlotData slot) => List.Remove(slot);
    }

}

