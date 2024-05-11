using System.Linq;
using Packages.UIController.Script.Animations;
using UnityEngine;

namespace Packages.UIController.Script.UI
{
    public abstract class BasePageUI : MonoBehaviour
    {
        public abstract PageType Type { get; }
        public Canvas rootCanvas;
        public GameObject root;

        public abstract void Init();

        public abstract void Show(BasePageUI page);

        public abstract void Hide(BasePageUI page);

        public void Show()
        {
            rootCanvas.enabled = true;
        }

        public void Hide()
        {
            rootCanvas.enabled = false;
        }
    }


    public enum PageType
    {
        None = 0,
        MainMenu = 1,
        Setting = 2,
        Game = 3,
        Client = 4,
    }
}