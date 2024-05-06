using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ScriptUI
{
    public class MainMenuPageUi : BasePageUI
    {
        // public event Action OnHidePage;
        public Button gamePage;
        public Button settingPage;
        public Button exitGameButton;

        public GameObject objectPage;


        [SerializeField] private Button backButton;

        public override PageType Type => PageType.MainMenu;

        public override void Show(BasePageUI page)
        {
            page.OpenPage(page);
            Debug.Log("here");
        }

        public override void Hide(BasePageUI page)
        {
            page.ClosePage(page);

        }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            settingPage.onClick.AddListener(OpenSetting);
            gamePage.onClick.AddListener(OpenGamePage);
            exitGameButton.onClick.AddListener(ExitGame);
        }

        private void OpenSetting()
        {
            var page = UIManager.instance.pages.FirstOrDefault(p => p.Type == PageType.Setting);
            if (page != null)
            {
                Debug.Log("here");
                var animationType = UIManager.instance.animationUiPages.FirstOrDefault(p => p.Type == AnimationType.MoveLeft);
                if (animationType != null)
                {
                    Show(page);
                    animationType.Show(page);
                }
            }
        }
        private void OpenGamePage()
        {

            var page = UIManager.instance.pages.FirstOrDefault(p => p.Type == PageType.Game);
            if (page != null)
            {
                Debug.Log("here");
                var animationType = UIManager.instance.animationUiPages.FirstOrDefault(p => p.Type == AnimationType.MoveLeft);
                if (animationType != null)
                {
                    Show(page);
                    animationType.Show(page);
                }
            }
        }

        private void ExitGame()
        {
            Debug.Log("here 11");

            Application.Quit();
        }
    }
}