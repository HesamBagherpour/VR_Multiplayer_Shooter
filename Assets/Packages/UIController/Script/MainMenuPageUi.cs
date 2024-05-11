using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ScriptUI
{
    public class MainMenuPageUI : BasePageUI
    {
        // public event Action OnHidePage;
        [SerializeField] private Button gamePage;
        [SerializeField] private Button settingPage;
        [SerializeField] private Button exitGameButton; 
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
            settingPage.onClick.AddListener(OpenPageSetting);
            gamePage.onClick.AddListener(OpenPageGame);
            exitGameButton.onClick.AddListener(ExitGame);

        }

        private void OpenPageSetting()
        {
            var page = UIManager.instance.pages.FirstOrDefault(p => p.Type == PageType.Setting);
            if (page != null)
            {
                Show(page);
            }

            if (page.uiPageMover != null)
            {
                page.uiPageMover.Show(page);
            }
        }

        private void OpenPageGame()
        {
            var page = UIManager.instance.pages.FirstOrDefault(p => p.Type == PageType.Game);
            if (page != null)
            {
                Show(page);
            }

            if (page.uiPageMover != null)
            {
                page.uiPageMover.Show(page);
            }
        }
        
        private void ExitGame()
        {
            Application.Quit();
        }
    }
}