using Game.Scripts.StatsCharacter;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GamePlay
{
    public static class UnitController
    {
        private static Dictionary<Vector3Int, CharacterBase> characterDictPos;
        
        public static void Init()
        {
            characterDictPos ??= new();
            characterDictPos.Clear();
        }

        public static CharacterBase GetCharacter(Vector3Int pos)
        {
            if(characterDictPos.TryGetValue(pos, out var character)) return character;
            return null;
        }

        public static void AddCharacter(Vector3Int pos, CharacterBase character)
        {
            if (characterDictPos.ContainsKey(pos)) return;
            characterDictPos[pos] = character;
        }

        public static void UpdatePosition(Vector3Int from, Vector3Int to)
        {
            if(from == to) return;
            CharacterBase character = GetCharacter(from);
            if(character == null) return;
            characterDictPos[to] = character;
            RemovePosition(from);
        }

        public static void RemovePosition(Vector3Int pos)
        {
            characterDictPos.Remove(pos);
        }

        public static void ClearAllSelectedDetection()
        {
            foreach(var character in characterDictPos.Values)
            {
                character.HUD.HideSelectedDetection();
            }
        } 
    }
}

