using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerSmartHatUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _buttonOnAndOff;

    public void Awake()
    {
        //s_buttonOnAndOff.onClick.AddListener(OffAndOnHat);   
        Debug.Log("on");
    }

    public void OffAndOnHat()
    {
        if (_canvas.enabled == true)
        {
            Debug.Log("ono" + _canvas.enabled);

            _canvas.enabled = false;
            return;
        }

        _canvas.enabled = true;
    }
}