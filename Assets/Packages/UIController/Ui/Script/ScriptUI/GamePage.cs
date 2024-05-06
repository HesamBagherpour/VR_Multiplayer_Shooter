using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace Script.ScriptUI
{
    public  class GamePage : BasePageUi
    {
        [SerializeField]  private Button backButton;
        public override PageType Type => PageType.Game;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            backButton.onClick.AddListener(ClosePage);
        }
        public override void Show(BasePageUi page)
        {
            
        }

        public override void Hide(BasePageUi page)
        {
            page.ClosePage(page);
        }
        private void ClosePage()
        {
            Debug.Log("this4");
          
            var page = UiManager.instance.pages.FirstOrDefault(p => p.Type == PageType.Game);
            if (page != null )
            {
                var animType = UiManager.instance.animationUiPages.FirstOrDefault( p=> p.Type == AnimationType.ScaleUp);
                if (animType!=null)
                {
                    animType.Close(page);
                }
             
            }
        }
    }
}
