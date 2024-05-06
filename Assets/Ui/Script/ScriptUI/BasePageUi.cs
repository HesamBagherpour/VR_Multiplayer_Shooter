using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;


namespace Script.ScriptUI
{
    public abstract class BasePageUi : MonoBehaviour

    {
        public abstract PageType Type { get; }


        public event Action<BasePageUi> OnShow;
        public event Action<BasePageUi> OnClose;

        void Start()
        {
            var page = UiManager.instance.pages.FirstOrDefault(p => p.Type == PageType.MainMenu);
            if (page != null && page.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                UiManager.instance.currentScreen = page;
                UiManager.instance.currentScreen.transform.SetAsLastSibling();
            }
        }

        public abstract void Show(BasePageUi page);

        public abstract void Hide(BasePageUi page);

        public void OpenPage(BasePageUi screenToChangeTo)
        {

            if (UiManager.instance.currentScreen != null)
            {
                UiManager.instance.currentScreen.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

            if (screenToChangeTo != null)
            {
                UiManager.instance.previousScreens = UiManager.instance.currentScreen;
                UiManager.instance.currentScreen = screenToChangeTo;
                UiManager.instance.currentScreen.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("here openpage");

            }
        }

        public void ClosePage(BasePageUi page)
        {
            Debug.Log("this0");

            UiManager.instance.currentScreen.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            UiManager.instance.currentScreen = UiManager.instance.previousScreens;
            UiManager.instance.currentScreen.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public enum PageType
    {
        None = 0,
        MainMenu = 1,
        Setting = 2,
        Game = 3
    }
}