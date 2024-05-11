using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.UIController.Script.UI
{
    public class SettingsPage : BasePageUI
    {
        [SerializeField] private Button backButton;

        public override PageType Type => PageType.Setting;


        public override void Show(BasePageUI page)
        {
        }

        public override void Hide(BasePageUI page)
        {
        }

        public override void Init()
        {
            Hide();
            backButton.onClick.AddListener(Back);
        }

        private void Back()
        {
            UIManager.Instance.ClosePage();
        }
    }
}