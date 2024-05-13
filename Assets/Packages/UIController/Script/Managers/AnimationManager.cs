﻿using Emaj.Patterns;
using Packages.UIController.Script.Animations;
using Packages.UIController.Script.Base;
using Packages.UIController.Script.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace Packages.UIController.Script.Managers
{
    public class AnimationManager : SingletonMonoBehaviour<AnimationManager>
    {
        public void HandleAnimation(ComponentType componentType, CurrentAnimationState state, PageType type)
        {
            var page = UIManager.Instance.pages.Find(t => t.Type == type);
            print("<color=blue> PAGE : " + page.transform.name + "</color>" + "<color=blue> ANIMATION : " + state +
                  "</color>");
            if (page == null)
                return;
            Debug.Log("dddddddddd" + state);
            var component = page.animationComponents.Find(t => t.Type == state);
            Debug.Log("dddddddddd" + component.Type);

            var failCondition = component == null || state == CurrentAnimationState.None;

            if (failCondition)
                return;

            if (state == CurrentAnimationState.StartAnimation)
            {
                print("<color=red> IS GOING TO StartAnimation  ssssssssssss" + page.transform.name + "</color>");

                component.Show();
                return;
            }

            print("<color=red> IS GOING TO END  state " + state + "</color>");

            if (state == CurrentAnimationState.EndAnimation && component.Type == CurrentAnimationState.EndAnimation)
            {
                print("<color=red> IS GOING TO END " + component.ComponentType + "</color>");
                component.Close();
            }
        }
    }
}