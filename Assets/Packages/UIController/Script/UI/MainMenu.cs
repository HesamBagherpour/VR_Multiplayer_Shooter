using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Packages.UIController.Script.Animations;
using Packages.UIController.Script.Base;
using Packages.UIController.Script.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.UIController.Script.UI
{
    public class MainMenu : PageBaseUI
    {
        [Header("Buttons")] [SerializeField] private Button gamePageButton;
        [SerializeField] private Button settingPageButton;
        [SerializeField] private Button exitGameButton;

        public override PageType Type => PageType.MainMenu;

        public override void Init()
        {
            HideRoot();
            settingPageButton.onClick.RemoveAllListeners();
            gamePageButton.onClick.RemoveAllListeners();
            exitGameButton.onClick.RemoveAllListeners();

            settingPageButton.onClick.AddListener(OpenSettingPage);
            gamePageButton.onClick.AddListener(OpenGamePage);
            exitGameButton.onClick.AddListener(ExitGame);
        }

        private void OpenSettingPage()
        {
            UIManager.Instance.OpenPage(PageType.Setting);
        }

        private void OpenGamePage()
        {
            print("Opening game page ");
            UIManager.Instance.OpenPage(PageType.Game);
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}