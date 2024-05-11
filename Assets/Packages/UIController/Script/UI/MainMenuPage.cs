using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.UIController.Script.UI
{
    public class MainMenuPage : BasePageUI
    {
        [SerializeField] private Button gamePageButton;
        [SerializeField] private Button settingPageButton;
        [SerializeField] private Button exitGameButton;
        public override PageType Type => PageType.MainMenu;

        public override void Show(BasePageUI page)
        {
        }

        public override void Hide(BasePageUI page)
        {
        }

        public override void Init()
        {
            Hide();
            settingPageButton.onClick.RemoveAllListeners();
            gamePageButton.onClick.RemoveAllListeners();
            exitGameButton.onClick.RemoveAllListeners();

            settingPageButton.onClick.AddListener(OpenSettingPage);
            gamePageButton.onClick.AddListener(OpenGamePage);
            exitGameButton.onClick.AddListener(ExitGame);
        }

        private void OpenSettingPage() => UIManager.Instance.OpenPage(PageType.Setting);
        private void OpenGamePage() => UIManager.Instance.OpenPage(PageType.Game);
        private void ExitGame() => Application.Quit();
    }
}