
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


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
       public List<BasePageUI> openPages=new List<BasePageUI>();
        public void Awake()
        {
            pages = new();
            animationUiPages = new List<BaseAnimationUiPage>();
            instance = this;
            GenratePageUi();
           //    firstPageUI.transform.SetAsLastSibling();
            // previousScreens = new List<BasePageUi>();
        }
        public void GenratePageUi()
        {
            for (int i = 0; i < pageContainer.listPageUi.Count; i++)
            {
                var myPage = Instantiate( pageContainer.listPageUi[i], transform.position, Quaternion.identity);
                pages.Add(myPage);

            }
           
           

           
        }


    }
}

