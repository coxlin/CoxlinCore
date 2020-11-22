using UnityEngine;


namespace CoxlinCore
{
    public static class AnimatorExtensions
    {
        public static bool AnimatorStateIsFinished(
         this Animator animator, string stateName, int layerIndex = 0)
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            return (animatorStateInfo.normalizedTime >= 1 && animatorStateInfo.IsName(stateName));
        }

        
        public static bool CurrentStateHasName(this Animator animator, string name, int layerID)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerID);
            int checkHash = Animator.StringToHash(name);
            bool result = stateInfo.shortNameHash == checkHash;
            return result;
        }

        
        public static void CrossFadeToStateIfNameIsDifferent(
            this Animator animator,
            string name,
            int layerId)
        {
            if (!animator.CurrentStateHasName(name, layerId))
            {
                animator.CrossFade(name, 0f);
            }
        }

        public static void SetValueForKey(this AnimationCurve curve, float time, float value)
        {
            for (int i = 0; i < curve.keys.Length; ++i)
            {
                if (curve.keys[i].time == time)
                {
                    curve.RemoveKey(i);
                    break;
                }
            }
            curve.AddKey(time, value);
        }

        public static void SetState(this Animator animator, string state)
        {
            animator.CrossFade(state, 0f);
        }
    }
}
