using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Packages.UIController.Script.Animations;
using Packages.UIController.Script.Base;
using UnityEngine;


namespace Packages.UIController.Script.ScriptableObjects
{
    [CreateAssetMenu(fileName = "DataAnimation", menuName = "ScriptableObjects/DataAnimation")]
    public class AnimationData : ScriptableObject
    {
        public List<AnimationBaseUI> listAnimationData = new();

        public AnimationBaseUI GetAnimationType(CurrentAnimationState type)
        {
            var page = listAnimationData.FirstOrDefault(p => p.Type == type);
            if (page is null) Debug.LogError($"There is no page with name {name}");

            return page;
        }
    }
}