using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VR_PROJECT;


namespace Packages.UIController.Script.UI
{
    public class ClientPage : BasePageUI
    {
        [SerializeField] private TMP_InputField _addressInput;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _backButton;

        public override PageType Type => PageType.Client;

        public override void Show(BasePageUI page)
        {
        }

        public override void Init()
        {
            Hide();
            _joinButton.onClick.AddListener(OnJoinButtonClick);
            _backButton.onClick.AddListener(Back);
        }

        private void Back()
        {
            UIManager.Instance.ClosePage();
        }

        private async void OnJoinButtonClick()
        {
            var result = await GameManager.Instance.NetworkController.ConnectClient(_addressInput.text);

            if (result.IsSuccess)
                UIManager.Instance.CloseAllPages();
        }

        public override void Hide(BasePageUI page)
        {
            //UIManager.Instance.ClosePage(page);
        }
    }
}