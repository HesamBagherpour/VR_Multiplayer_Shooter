using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ScriptUI
{
    public class MainMenuPageUi : BasePageUi
    {
        // public event Action OnHidePage;
        public Button gamePage;
        public Button settingPage;
        public GameObject objectPage;


        [SerializeField] private Button backButton;

        public override PageType Type => PageType.MainMenu;

        public override void Show(BasePageUi page)
        {
            page.OpenPage(page);
            Debug.Log("here");
        }

        public override void Hide(BasePageUi page)
        {
        }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            settingPage.onClick.AddListener(OpenSetting);
            gamePage.onClick.AddListener(OpenGamePage);
        }

        private void OpenSetting()
        {
            var page = UiManager.instance.pages.FirstOrDefault(p => p.Type == PageType.Setting);
            if (page != null)
            {
                var animType = UiManager.instance.animationUiPages.FirstOrDefault( p=> p.Type == AnimationType.MoveLeft);

                if (animType != null)
                {
                    Show(page);
                    animType.Show(page);
                }
            }
        }
        private void OpenGamePage()
        {

            var page = UiManager.instance.pages.FirstOrDefault(p => p.Type == PageType.Game);
            if (page != null)
            {
                Debug.Log("here");
                var animationType = UiManager.instance.animationUiPages.FirstOrDefault(p => p.Type == AnimationType.MoveLeft);
                if (animationType != null)
                {
                    Show(page);
                    animationType.Show(page);
                    Debug.Log("here 11");
                }
            }
        }
    }
}