using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ScriptUI
{
    public class PageSetting : BasePageUI
    {
          [SerializeField]  private Button buttonBack;
          public override PageType Type => PageType.Setting;
        public void ShowPage()
        {
            // LinkerScriptUi.instance.managerPageUi.ChangeToScreen(this.gameObject);
        }



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
            buttonBack.onClick.AddListener(ButtonClosePage);

        }
     
       

        private void ButtonClosePage()
        {
            if (this.uiPageScaler != null)
            {
                this.uiPageScaler.Show(this);
            }
        }
    }
}
