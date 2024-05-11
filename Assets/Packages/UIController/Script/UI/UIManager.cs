using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Emaj.Patterns;
using Packages.UIController.Script.Animations;
using Packages.UIController.Script.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;


namespace Packages.UIController.Script.UI
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        public BasePageUI currentPage;
        public List<BasePageUI> pages;
        [SerializeField] private BasePageUI firstPageUI;
        public BasePageUI previousPage;
        public List<BasePageUI> openPages = new List<BasePageUI>();
        public  List<BaseAnimationUIPage> baseAnimationList =new();

        private void Awake()
        {
            Init(PageType.MainMenu);
        }

        private async void Init(PageType type)
        {
            currentPage = firstPageUI = pages.Find(t => t.Type == type);
            pages.ForEach(t => t.Init());
            PopupUI.Instance.Init();
            await UniTask.Delay(100);
            currentPage.Show();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                PopupUI.Instance.ShowMessage($"Message_{Random.Range(0, 100)}", () => { print("hello muhammad"); });
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                PopupUI.Instance.ShowMessage($"Message_{Random.Range(100, 200)}", () => { print("Yellow Jaffar"); });
            }
        }

        public void OpenPage(PageType type)
        {
            var page = pages.Find(t => t.Type == type);
            if (page == null)
                return;
            if (currentPage == null)
                return;
            currentPage.Hide();
            previousPage = currentPage;
            openPages.Add(currentPage);
            currentPage = page;
            currentPage.Show();
            Debug.Log("open");
        }

        public void ClosePage()
        {
            Debug.Log("// "+previousPage);

            currentPage.Hide();
            currentPage = previousPage;
            if (openPages.Count > 0)
            {
                openPages.Remove(openPages[^1]);
            }

            currentPage.Show();
        }

        public void CloseAllPages()
        {
            currentPage.root.SetActive(false);
            currentPage = null;
            openPages.Clear();
        }
    }
}