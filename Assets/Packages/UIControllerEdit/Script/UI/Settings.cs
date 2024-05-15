using System.Collections;
using System.Linq;
using Packages.UIController.Script.Animations;
using Packages.UIController.Script.Base;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.UIController.Script.UI
{
    public class Settings : PageBaseUI
    {
        public bool isFinishedAnim = false;

        [SerializeField] private Button backButton;
        public override PageType Type => PageType.Setting;

        public override void Init()
        {
            HideRoot();
            backButton.onClick.AddListener(Back);
        }

        private void Back()
        {
            UIManager.Instance.ClosePage();
        }
    }
}