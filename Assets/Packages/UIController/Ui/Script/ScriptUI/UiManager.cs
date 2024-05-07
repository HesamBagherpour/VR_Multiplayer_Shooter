using System.Collections.Generic;
using UnityEngine;
using Packages.UIController;

namespace Script.ScriptUI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        public BasePageUI currentScreen;
        public PageContainer pageContainer;
        public AnimationData animationData;

        public List<BasePageUI> pages;
        public List<BaseAnimationUiPage> animationUiPages;

        public BasePageUI previousScreens;
        [SerializeField] private GameObject subAnimationObject;

        public void Awake()
        {
            pages = new();
            animationUiPages = new List<BaseAnimationUiPage>();
            instance = this;
            GenratePageUi();
            // previousScreens = new List<BasePageUi>();
        }

        public void GenratePageUi()
        {
            for (int i = 0; i < pageContainer.listPageUi.Count; i++)
            {
                var myPage = Instantiate(pageContainer.listPageUi[i], transform.position, Quaternion.identity);
                pages.Add(myPage);
            }

            for (int i = 0; i < animationData.listAnimationData.Count; i++)
            {
                var myPage = Instantiate(animationData.listAnimationData[i], transform.position, Quaternion.identity);
                myPage.transform.SetParent(subAnimationObject.transform);
                animationUiPages.Add(myPage);
            }
        }
    }
}