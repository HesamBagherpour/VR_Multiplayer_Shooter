using System.Linq;
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
            var page = UIManager.Instance.pages.FirstOrDefault(p => p.Type == PageType.Setting);
            if (page != null)
            {
                var animType = UIManager.Instance.animationUiPages.FirstOrDefault(p => p.Type == AnimationType.ScaleUp);
                if (animType != null)
                {
                    animType.Close(page);
                }
            }
        }
    }
}