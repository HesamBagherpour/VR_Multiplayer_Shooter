using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Packages.UIController.Script.Animations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using AnimationState = Packages.UIController.Script.Animations.AnimationState;

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

        private void OpenGamePage()
        {
            var page = UIManager.Instance.pages.Find(t => t.Type == PageType.Game);

            if (page.gameObject.TryGetComponent<ScaleAnimationHandler>(out ScaleAnimationHandler scaleAnimationHandler))
            {
                // Check if the scale animation has ended
                if (scaleAnimationHandler.state == AnimationState.StartAnimation)
                {
                    // Show the page and start the scale animation
                    scaleAnimationHandler.Show(PageType.Game);
                    StartCoroutine(ShowPageScaleAnimation(scaleAnimationHandler));
                }
            }

            if (page.gameObject.TryGetComponent<MoveAnimationHandler>(out MoveAnimationHandler moveAnimationHandler))
            {
                if (moveAnimationHandler.state == AnimationState.StartAnimation)
                {
                    moveAnimationHandler.Show(PageType.Game);
                    StartCoroutine(ShowPageMoveAnimation(moveAnimationHandler));
                }
            }
        }

        private IEnumerator ShowPageScaleAnimation(ScaleAnimationHandler scaleAnimationHandler)
        {
            yield return new WaitUntil(() => scaleAnimationHandler.isFinished == true);
            UIManager.Instance.OpenPage(PageType.Game);
            Debug.Log("Opening page with scale animation");
        }

        private IEnumerator ShowPageMoveAnimation(MoveAnimationHandler moveAnimationHandler)
        {
           
                yield return new WaitUntil(() => moveAnimationHandler.isFinished == true);
                UIManager.Instance.OpenPage(PageType.Game);
                Debug.Log("sssss");
            
        }

        private void ExitGame() => Application.Quit();
    }
}