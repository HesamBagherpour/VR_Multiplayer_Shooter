using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace Script.ScriptUI
{
    public  class GamePage : BasePageUI
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
        public override void Show(BasePageUI page)
        {
            
        }

        public override void Hide(BasePageUI page)
        {
            page.ClosePage(page);
        }
        private void ClosePage()
        {
            var page = UIManager.instance.pages.FirstOrDefault(p => p.Type == PageType.Game);
            if (page != null )
            {
                var animType = UIManager.instance.animationUiPages.FirstOrDefault( p=> p.Type == AnimationType.ScaleUp);
                if (animType!=null)
                {
                    animType.Close(page);
                }
             
            }
        }
    }
}
