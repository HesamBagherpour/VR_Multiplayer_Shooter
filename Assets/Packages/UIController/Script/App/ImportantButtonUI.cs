using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ImportantButtonUI : MonoBehaviour
{
    public Canvas rootCanvas;
    public Button removeHelmetBtn;
    private InputAction _inputAction;

    public void Init()
    {
        rootCanvas.enabled = true;
        removeHelmetBtn.onClick.RemoveAllListeners();
        removeHelmetBtn.onClick.AddListener(() =>
        {
            if (HelmetUI.Instance)
                HelmetUI.Instance.HandleEnabled();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (HelmetUI.Instance)
                HelmetUI.Instance.HandleEnabled();

       
    }
}

