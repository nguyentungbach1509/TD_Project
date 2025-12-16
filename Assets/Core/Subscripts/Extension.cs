using System.Collections.Generic;
using UnityEngine;

namespace Subscripts.Extensions
{
    public static class Extension
    {
        public static void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }

    public static class Vector3IntExt
    {
        public static readonly Vector3Int LeftDown = new Vector3Int(-1, -1, 0);
        public static readonly Vector3Int LeftUp = new Vector3Int(-1, 1, 0);
        public static readonly Vector3Int RightDown = new Vector3Int(1, -1, 0);
        public static readonly Vector3Int RightUp = new Vector3Int(1, 1, 0);
    }

}


