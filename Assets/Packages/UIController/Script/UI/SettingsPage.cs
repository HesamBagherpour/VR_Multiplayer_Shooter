using System.Collections;
using System.Linq;
using Packages.UIController.Script.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.UIController.Script.UI
{
    public class SettingsPage : BasePageUI
    {
        public bool isFinishedAnim = false;

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
            if (this.gameObject.TryGetComponent<ScaleAnimationHandler>(out ScaleAnimationHandler scaleAnimationHandler))
            {
                scaleAnimationHandler.Close(PageType.Setting);
                StartCoroutine(BackRoutine());
            }
          
        }

        private IEnumerator BackRoutine()
        {
            if (this.gameObject.TryGetComponent<ScaleAnimationHandler>(out ScaleAnimationHandler scaleAnimationHandler))
            {
                yield return new WaitUntil(() => scaleAnimationHandler.isFinished == true);
                    UIManager.Instance.ClosePage();
                
            }
        }
    }
}

