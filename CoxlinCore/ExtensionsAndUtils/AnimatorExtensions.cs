/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using UnityEngine;

namespace CoxlinCore
{
    public static class AnimatorExtensions
    {
        public static bool AnimatorStateIsFinished(
            this Animator animator, string stateName, int layerIndex = 0)
        {
            var animatorStateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            return animatorStateInfo.IsName(stateName) && animatorStateInfo.normalizedTime >= 1;
        }
        
        public static bool CurrentStateHasName(this Animator animator, string name, int layerIndex = 0)
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);
            int checkHash = Animator.StringToHash(name);
            bool result = stateInfo.shortNameHash == checkHash;
            return result;
        }
        
        public static void CrossFadeToStateIfNameIsDifferent(
            this Animator animator,
            string name,
            int layerIndex = 0,
            float normalizedTransistionDuration = 0.0f)
        {
            if (!animator.CurrentStateHasName(name, layerIndex))
            {
                animator.CrossFade(name, normalizedTransistionDuration);
            }
        }
    }
}