using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.ScriptUI
{
    public class GamePage : BasePageUI
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button buttonSrever;
        [SerializeField] private Button buttonClient;
        [SerializeField] private ClientPage clientPage;
        public override PageType Type => PageType.Game;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            backButton.onClick.AddListener(ButtonClosePage);
            buttonClient.onClick.AddListener(OpenPageClient);

        }

        public override void Show(BasePageUI page)
        {
            page.OpenPage(clientPage);

        }
        private void OpenPageClient()
        {
            if (clientPage != null)
            {
                Show(clientPage);
            }

            if (clientPage.uiPageMover != null)
            {
                clientPage.uiPageMover.Show(clientPage);
            }
        }
        public override void Hide(BasePageUI page)
        {
            page.ClosePage(page);
            Debug.Log("sss");

        }

        private void ButtonClosePage()
        {
            if (this.uiPageScaler != null)
            {
               Hide(this);
               
            }
        }
    }
}