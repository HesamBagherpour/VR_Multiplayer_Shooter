using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Packages.UIController.Script.Animations;
using UnityEngine;
using AnimationState = Packages.UIController.Script.Animations.AnimationState;


namespace Packages.UIController.Script.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DataAnimation", menuName = "ScriptableObjects/DataAnimation")]
    public class AnimationData : ScriptableObject
    {
        public List<BaseAnimationUIPage> listAnimationData = new List<BaseAnimationUIPage>();

        public BaseAnimationUIPage GetAnimationType(AnimationState type)
        {
            var page = listAnimationData.FirstOrDefault(p => p.Type == type);
            if (page is null)
            {
                Debug.LogError($"There is no page with name {name}");
            }

            return page;
        }
    }
}