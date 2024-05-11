using System.Collections;
using System.Collections.Generic;
using Script.ScriptUI;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VR_PROJECT;

public class ClientPage : BasePageUI
{
    [SerializeField] private TMP_InputField _addressInput;
    [SerializeField] private Button _joinButton;
    [SerializeField] private Button _backButton;

    private BasePageUI _basePageUIImplementation;
    public override PageType Type => _basePageUIImplementation.Type;
    public PageType PageType;
    public override void Show(BasePageUI page)
    {
        _basePageUIImplementation.Show(page);
    }
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _joinButton.onClick.AddListener(OnJoinButtonClick);
        _backButton.onClick.AddListener(ButtonClosePage);

    }

    private async void OnJoinButtonClick()
    {
       var result = await GameManager.Instance.NetworkController.ConnectClient(_addressInput.text);
       
       if(result.IsSuccess)
           CloseAllPages();
    }

    public override void Hide(BasePageUI page)
    {
        page.ClosePage(page);
    }

    private void ButtonClosePage()
    {
        if (this.uiPageScaler != null)
        {
            this.uiPageScaler.Show(this);
        }
    }
    
    
}
