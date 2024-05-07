using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;


namespace Script.ScriptUI
{
    public abstract class BasePageUI : MonoBehaviour

    {
        public abstract PageType Type { get; }

        public RectTransform rootObject;
        public CustomAnimationHandler AnimationHandler;
        public event Action<BasePageUI> OnShow;
        public event Action<BasePageUI> OnClose;


        void Start()
        {
            var page = UIManager.Instance.pages.FirstOrDefault(p => p.Type == PageType.MainMenu);
            if (page != null && page.gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                UIManager.Instance.currentScreen = page;
                UIManager.Instance.currentScreen.transform.SetAsLastSibling();
            }
        }

        public abstract void Show(BasePageUI page);

        public abstract void Hide(BasePageUI page);

        public void OpenPage(BasePageUI screenToChangeTo)
        {
            if (UIManager.Instance.currentScreen != null)
            {
                UIManager.Instance.currentScreen.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }

            if (screenToChangeTo != null)
            {
                UIManager.Instance.previousScreens = UIManager.Instance.currentScreen;
                UIManager.Instance.currentScreen = screenToChangeTo;
                UIManager.Instance.currentScreen.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log("here openpage");
            }
        }

        public void ClosePage(BasePageUI page)
        {
            Debug.Log("this0");

            UIManager.Instance.currentScreen.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            UIManager.Instance.currentScreen = UIManager.Instance.previousScreens;
            UIManager.Instance.currentScreen.gameObject.transform.GetChild(0).gameObject.SetActive(true);
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