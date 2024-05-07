using System;
using System.Linq;
using DG.Tweening;
using Packages.UIController;
using Packages.UIController.Utils;
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
            var page = UIManager.Instance.pages.FirstOrDefault(p => p.Type == PageType.Setting);

            if (page == null)
                return;

            var pageRoot = page.rootObject;
            Debug.Log("Opening Settings");
            Show(page);
            //page.AnimationHandler.DoMove();
            pageRoot.transform.DoCustomMove(
                CustomAnimationData.Instance.MyAnimations.Find(t =>
                    t.animationType == MyAnimation.AnimationType.MoveLeft), false);
        }

        private void OpenGamePage()
        {
            var page = UIManager.Instance.pages.FirstOrDefault(p => p.Type == PageType.Game);
            var pageRoot = page.rootObject;
            if (page == null)
                return;
            Debug.Log("Opening Game");
            Show(page);

            pageRoot.DoCustomMove(CustomAnimationData.Instance.MyAnimations.Find(t =>
                t.animationType == MyAnimation.AnimationType.MoveCenter));
        }

        private void ExitGame()
        {
            Debug.Log("ExitGame");
            Application.Quit();
        }
    }
}