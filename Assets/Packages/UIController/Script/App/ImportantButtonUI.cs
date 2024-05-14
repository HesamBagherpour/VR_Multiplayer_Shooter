using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImportantButtonUI : MonoBehaviour
{
    public Canvas rootCanvas;
    public Button removeHelmetBtn;
    public void Init()
    {
        rootCanvas.enabled = true;
        removeHelmetBtn.onClick.RemoveAllListeners();
        removeHelmetBtn.onClick.AddListener(() =>
        {
            if(HelmetUI.Instance)
                HelmetUI.Instance.HandleEnabled();
        });
    }
    
}
