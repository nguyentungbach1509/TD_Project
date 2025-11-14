using SubScripts.Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SubScripts
{
    public enum AnimationEventType
    {
        Started,
        Hit,
        Finished,
        // Thêm các loại event khác nếu cần
    }

    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] SpriteRenderer spriteRenderer;

        // Dictionary lưu các event cho từng loại animation
        private Dictionary<AnimationKey, Dictionary<AnimationEventType, Action>> animationEvents
            = new Dictionary<AnimationKey, Dictionary<AnimationEventType, Action>>();

        public void PlayAnimation(AnimationKey key)
        {
            animator.SetTrigger(key.Hash());
        }

        public void PlayAnimation(string key, int value)
        {
            animator.SetInteger(key, value);
        }

        public void PlayBlendAnimation(AnimationKey key, float blendTime)
        {
            animator.SetFloat(key.Hash(), blendTime);
        }

        public AnimatorStateInfo AnimationStateInfo()
        {
            return animator.GetCurrentAnimatorStateInfo(0);
        }

        // Đăng ký event cho một animation cụ thể
        public void RegisterAnimationEvent(AnimationKey key, AnimationEventType eventType, Action callback)
        {
            if (!animationEvents.ContainsKey(key))
            {
                animationEvents[key] = new Dictionary<AnimationEventType, Action>();
            }

            animationEvents[key][eventType] = callback;
        }

        // Hủy đăng ký event
        public void UnregisterAnimationEvent(AnimationKey key, AnimationEventType eventType)
        {
            if (animationEvents.ContainsKey(key) && animationEvents[key].ContainsKey(eventType))
            {
                animationEvents[key].Remove(eventType);
            }
        }

        // Method này sẽ được gọi từ Animation Event trong Animator
        public void OnAnimationEvent(string eventData)
        {
            // eventData format: "AnimationKey:EventType"
            string[] data = eventData.Split(':');
            if (data.Length != 2) return;

            if (Enum.TryParse(data[0], out AnimationKey key) &&
                Enum.TryParse(data[1], out AnimationEventType eventType))
            {
                if (animationEvents.ContainsKey(key) &&
                    animationEvents[key].ContainsKey(eventType))
                {
                    animationEvents[key][eventType]?.Invoke();
                }
            }
        }

        // Tiện ích để kiểm tra trạng thái animation
        public bool IsAnimationFinished(AnimationKey key)
        {
            var currentInfo = animator.GetCurrentAnimatorStateInfo(0);
            return currentInfo.IsName(key.ToString()) && currentInfo.normalizedTime >= 1f;
        }

    }
}


