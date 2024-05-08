using System.Linq;
using DG.Tweening;
using Packages.UIController.Utils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.ScriptUI
{
    public class GamePage : BasePageUI
    {
        [SerializeField] private Button backButton;
        public override PageType Type => PageType.Game;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            backButton.onClick.AddListener(ClosePage);
        }

        public override void Show(BasePageUI page)
        {
        }

        public override void Hide(BasePageUI page)
        {
            page.ClosePage(page);
        }

        private void ClosePage()
        {
            var page = UIManager.Instance.pages.FirstOrDefault(p => p.Type == PageType.Game);
            var prvScreen = UIManager.Instance.previousScreens;

            if (page == null)
                return;
            var pageRoot = page.rootObject;
            Hide(page);
            pageRoot.DOLocalMoveX(1900, 0.2f);
            prvScreen.rootObject.DOScaleY(1, 0.2f);
        }
    }
}