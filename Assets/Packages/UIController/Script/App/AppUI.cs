using System.Collections;
using System.Collections.Generic;
using Emaj.Patterns;
using Packages.UIController.Script.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Packages.UIController.Script.App
{
    public class AppUI : SingletonMonoBehaviour<AppUI>
    {
        [Header("Self Properties")] [SerializeField]
        private Canvas rootCanvas;

        [SerializeField] private GameObject root;

        [Header("Child Properties")] [SerializeField]
        private Canvas loadingOverlay;

        [SerializeField] private PopupUI popupUI;
        [SerializeField] private ImportantButtonUI importantButtonUI;


        public void Awake()
        {
            Init();
        }

        public void Init()
        {
            loadingOverlay.enabled = false;
            popupUI.Init();
            importantButtonUI.Init();
        }

        public void ShowMessage(string message)
        {
            popupUI.ShowMessage(message);
        }

        public void ShowPrompt(string message, UnityAction customAction)
        {
            popupUI.ShowMessage(message, customAction);
        }

        public void ShowLoading()
        {
            loadingOverlay.enabled = true;
        }

        public void HideLoading()
        {
            loadingOverlay.enabled = false;
        }

        public void HandleLoading(bool condition)
        {
            loadingOverlay.enabled = condition;
        }
    }
}