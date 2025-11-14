using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubScripts.Constants
{
    public enum AnimationKey
    {
        Atk,
        Idle,
        Move,
        StartAtk,
        DoAtk,
        EndAtk,
        Hit,
        Buff,
        JumpInit,
        Jumping,
        JumpLanding,
        Jump,
        Die,
        Teleport,
        Show,
        Hook,
        Spell,
        SpellAtk,
        SpellSub,
        Build,
    }

    public static class AnimationConstants
    {
        private static readonly Dictionary<AnimationKey, int> HashIds = new Dictionary<AnimationKey, int>();

        // Khởi tạo các hash ID
        static AnimationConstants()
        {
            foreach (AnimationKey key in Enum.GetValues(typeof(AnimationKey)))
            {
                HashIds[key] = Animator.StringToHash(key.ToString());
            }
        }

        // Lấy Hash ID từ AnimationKey
        public static int GetHash(AnimationKey key)
        {
            return HashIds[key];
        }

        // Extension method để dễ sử dụng
        public static int Hash(this AnimationKey key)
        {
            return GetHash(key);
        }
    }
}