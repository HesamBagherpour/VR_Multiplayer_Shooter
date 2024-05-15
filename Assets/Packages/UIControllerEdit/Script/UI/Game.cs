using System;
using System.Linq;
using Packages.UIController.Script.Base;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VR_PROJECT;

namespace Packages.UIController.Script.UI
{
    public class Game : PageBaseUI
    {
        [SerializeField] private Button backButton;
        [SerializeField] private Button serverButton;
        [SerializeField] private Button clientButton;
        [SerializeField] private Vector3 initScaleState;
        public bool isFinishedAnim = false;
        public override PageType Type => PageType.Game;

        public override void Init()
        {
            HideRoot();
            serverButton.onClick.AddListener(OnServerButtonClick);
            backButton.onClick.AddListener(Back);
            clientButton.onClick.AddListener(() => { UIManager.Instance.OpenPage(PageType.Client); });
            root.transform.localScale = initScaleState;
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