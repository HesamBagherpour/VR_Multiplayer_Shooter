using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VR_PROJECT;

namespace Packages.UIController.Script.UI
{
    public class GamePage : BasePageUI
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button serverButton;
        [SerializeField] private Button clientButton;
        [SerializeField] private ClientPage clientPage;
        public override PageType Type => PageType.Game;

        public override void Init()
        {
            Hide();
            serverButton.onClick.AddListener(OnServerButtonClick);
            backButton.onClick.AddListener(Back);
            clientButton.onClick.AddListener(() => { UIManager.Instance.OpenPage(PageType.Client); });
        }

        public override void Show(BasePageUI page)
        {
        }

        public override void Hide(BasePageUI page)
        {
            //UIManager.Instance.ClosePage(page);
        }

        public void Back()
        {
            UIManager.Instance.ClosePage();
        }

        private async void OnServerButtonClick()
        {
            GameManager.Instance.NetworkController.ConnectServer();
            var result = await GameManager.Instance.NetworkController.ConnectClient();

            if (result.IsSuccess)
                UIManager.Instance.CloseAllPages();
        }
    }
}