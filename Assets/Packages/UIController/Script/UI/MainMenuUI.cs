using System.Collections;
using System.Collections.Generic;
using Emaj.Patterns;
using UnityEngine;

public class MainMenuUI : SingletonMonoBehaviour<MainMenuUI>
{
    public Canvas rootCanvas;
    public Transform target;
    public Vector3 positionToOpen;
    public void Init(Transform _target)
    {
        //rootCanvas.enabled = false;
        //target = _target;
    }

    public void Show()
    {
        rootCanvas.transform.position = target.position + positionToOpen;
        rootCanvas.enabled = true;
    }

    public void Hide()
    {
        rootCanvas.enabled = false;
    }

    public bool IsActive() => rootCanvas.enabled;
}
