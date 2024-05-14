using System.Collections;
using System.Collections.Generic;
using Emaj.Patterns;
using UnityEngine;

public class HelmetUI : SingletonMonoBehaviour<HelmetUI>
{
    [SerializeField] private Canvas rootCanvas;

    public void HandleEnabled()
    {
        if (rootCanvas.enabled)
        {
            rootCanvas.enabled = false;
            return;
        }

        rootCanvas.enabled = true;
    }
}
