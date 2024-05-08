using System.Collections;
using System.Collections.Generic;
using Script.ScriptUI;
using UnityEngine;
using UnityEngine.UI;

public class ClientPage : BasePageUI
{
    [SerializeField] private Button backButton;

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
        backButton.onClick.AddListener(ButtonClosePage);

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
