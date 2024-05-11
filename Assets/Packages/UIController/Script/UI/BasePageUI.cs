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
        public Vector3 defaultScale;
        public Vector3 defaultPosition;

        public abstract void Init();

        public abstract void Show(BasePageUI page);

        public abstract void Hide(BasePageUI page);

        public void Show()
        {
            rootCanvas.enabled = true;
            Debug.Log(rootCanvas.name);
        }

        public void Hide()
        {
            rootCanvas.enabled = false;
        }

        public void ResetAnimation()
        {
            root.transform.localScale = defaultScale;
            root.transform.localPosition = defaultPosition;
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