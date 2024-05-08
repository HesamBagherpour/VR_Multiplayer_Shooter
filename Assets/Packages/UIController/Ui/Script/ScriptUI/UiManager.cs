
using System.Collections.Generic;
using UnityEngine;


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
        [SerializeField] private GameObject firstPageUI;
        public BasePageUI previousScreens;
        [SerializeField] private GameObject subAnimationObject;
        public void Awake()
        {
            pages = new();
            animationUiPages = new List<BaseAnimationUiPage>();
            instance = this;
            GenratePageUi();
            firstPageUI.transform.SetAsLastSibling();
            pages = pageContainer.listPageUi;
            // previousScreens = new List<BasePageUi>();
        }
        public void GenratePageUi()
        {
           
            for (int i = 0; i < animationData.listAnimationData.Count; i++)
            {
                var myPage = Instantiate(animationData.listAnimationData[i], transform.position, Quaternion.identity);
                myPage.transform.SetParent(subAnimationObject.transform);
                animationUiPages.Add(myPage);
            }
        }


    }
}

