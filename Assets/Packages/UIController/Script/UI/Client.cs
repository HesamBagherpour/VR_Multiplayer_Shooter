using System.Collections;
using System.Collections.Generic;
using Packages.UIController.Script.App;
using Packages.UIController.Script.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VR_PROJECT;


namespace Packages.UIController.Script.UI
{
    public class Client : PageBaseUI
    {
        [SerializeField] private TMP_InputField _addressInput;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private NumPadUI numberpad;
        public override PageType Type => PageType.Client;

        public override void Init()
        {
            HideRoot();
            _joinButton.onClick.AddListener(OnJoinButtonClick);
            _backButton.onClick.AddListener(Back);
            numberpad.Init();
            _addressInput.text = "192.168.1.1";
        }

        private void Back()
        {
            UIManager.Instance.ClosePage();
        }

        private async void OnJoinButtonClick()
        {
            AppUI.Instance.ShowLoading();

            var result = await GameManager.Instance.NetworkController.ConnectClient(_addressInput.text);
            if (!result.IsSuccess)
            {
                AppUI.Instance.ShowMessage(result.ErrorMessage);
                return;
            }

            AppUI.Instance.HideLoading();
            UIManager.Instance.CloseAllPages();
        }
    }
}