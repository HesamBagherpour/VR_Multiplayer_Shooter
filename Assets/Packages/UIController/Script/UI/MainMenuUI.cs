using System.Collections;
using System.Collections.Generic;
using Emaj.Patterns;
using UnityEngine;

public class MainMenuUI : SingletonMonoBehaviour<MainMenuUI>
{
    public Canvas rootCanvas;
    public Transform target;
    public Vector3 positionToOpen;
    public LookAtPlayerUI lookAtPlayer;

    public void Init(Transform _target)
    {
//        rootCanvas.enabled = false;
        //  target = _target;
        //    lookAtPlayer = GetComponent<LookAtPlayerUI>();
        //    lookAtPlayer.Init(target);
    }

    public void Show()
    {
        transform.position = target.position + positionToOpen;
        rootCanvas.enabled = true;
    }

    public void Hide()
    {
        rootCanvas.enabled = false;
    }

    public bool IsActive()
    {
        return rootCanvas.enabled;
    }
}