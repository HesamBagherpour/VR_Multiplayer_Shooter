using System.Collections.Generic;
using Emaj.Patterns;
using UnityEngine;
using Packages.UIController;

namespace Script.ScriptUI
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        public BasePageUI currentScreen;
        public CustomAnimationData AnimationData;

        public List<BasePageUI> pages;
        public List<BaseAnimationUiPage> animationUiPages;

        public BasePageUI previousScreens;
        [SerializeField] private GameObject subAnimationObject;
    }
}