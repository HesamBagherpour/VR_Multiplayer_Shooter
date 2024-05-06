using UnityEngine;
using UnityEngine.UI;

namespace Script.ScriptUI
{
    public class PageSetting : BasePageUi
    {
        public Button buttonShow;
        public Button buttonBack;

        public void ShowPage()
        {
            // LinkerScriptUi.instance.managerPageUi.ChangeToScreen(this.gameObject);
        }


        public override PageType Type => PageType.Setting;
        public override void Show(BasePageUi page)
        {
            throw new System.NotImplementedException();
        }

        public override void Hide(BasePageUi page)
        {
            throw new System.NotImplementedException();
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
          //  ManagerPageUi.instance.HideLastPage(PageType.Setting);

        }
    
    }
}
