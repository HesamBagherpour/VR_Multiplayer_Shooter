using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ScriptUI
{
    public class PageSetting : BasePageUI
    {
        public Button buttonShow;
        public Button buttonBack;

        public void ShowPage()
        {
            // LinkerScriptUi.instance.managerPageUi.ChangeToScreen(this.gameObject);
        }


        public override PageType Type => PageType.Setting;

        public override void Show(BasePageUI page)
        {
            throw new System.NotImplementedException();
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
            buttonBack.onClick.AddListener(ClosePage);
        }


        private void ClosePage()
        {
            var prvScreen = UIManager.Instance.previousScreens;
            Hide(this);
            rootObject.DOLocalMoveX(-1900, 0.2f);
            prvScreen.rootObject.DOScaleY(1, 0.2f);
        }
    }
}