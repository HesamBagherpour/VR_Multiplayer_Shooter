using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Packages.UIController.Script.Managers;
using UnityEngine;
using UnityEngine.Rendering;

namespace Packages.UIController.Script.Base
{
    public abstract class PageBaseUI : MonoBehaviour
    {
        public abstract PageType Type { get; }
        [Header("Page Properties")] public Canvas rootCanvas;
        public GameObject root;

        [Header("Animation Properties")] public Vector3 defaultScale;
        public Vector3 defaultPosition;
        public List<AnimationBaseUI> animationComponents = new();


        public abstract void Init();

        public async void Show()
        {
            rootCanvas.enabled = true;
            var startComponent = animationComponents.Find(t => t.Type == CurrentAnimationState.StartAnimation);
            if (startComponent == null)
                return;
            AnimationManager.Instance.HandleAnimation(startComponent.ComponentType, startComponent.Type, Type);
            await UniTask.WaitUntil(() => startComponent.isFinished);
            startComponent.isFinished = false;
        }

        public async void Hide()
        {
            var endComponent = animationComponents.Find(t => t.Type == CurrentAnimationState.EndAnimation);
            print(animationComponents.Count);

            if (endComponent == null)
            {
                HideRoot();
                return;
            }

            print(endComponent.ComponentType + "    " + endComponent.Type);
            AnimationManager.Instance.HandleAnimation(endComponent.ComponentType, CurrentAnimationState.EndAnimation,
                Type);
            await UniTask.WaitUntil(() => endComponent.isFinished);
            endComponent.isFinished = false;
            print("Animation ended");
            ResetAnimation();
            HideRoot();
        }

        public void ResetAnimation()
        {
            root.transform.localScale = defaultScale;
            root.transform.localPosition = defaultPosition;
        }

        public void HideRoot()
        {
            rootCanvas.enabled = false;
        }
    }


    public enum PageType
    {
        None = 0,
        MainMenu = 1,
        Setting = 2,
        Game = 3,
        Client = 4
    }
}