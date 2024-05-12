using Emaj.Patterns;
using Packages.UIController.Script.Animations;
using Packages.UIController.Script.Base;
using Packages.UIController.Script.UI;
using UnityEngine;

namespace Packages.UIController.Script.Managers
{
    public class AnimationManager : SingletonMonoBehaviour<AnimationManager>
    {
        public void HandleAnimation(ComponentType componentType, CurrentAnimationState state, PageType type)
        {
            
            var page = UIManager.Instance.pages.Find(t => t.Type == type);
            print( "<color=blue> PAGE : "+ page.transform.name + "</color>" + "<color=blue> ANIMATION : "+ state + "</color>");
            if(page == null)
                return;
            
            var component = page.animationComponents.Find(t => t.ComponentType == componentType);
            var failCondition = (component == null) || state == CurrentAnimationState.None;
           
            if(failCondition)
                return;

            if (state == CurrentAnimationState.StartAnimation)
            {
                component.Show();
                return;
            }
            if (state == CurrentAnimationState.EndAnimation)
            {
                print("<color=red> IS GOING TO END " + page.transform.name + "</color>");
                component.Close();
                return;
            }
        }
    }
}