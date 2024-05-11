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

        private void Awake()
        {
            Init(PageType.MainMenu);
        }

        private async void Init(PageType type)
        {
            currentPage = firstPageUI = pages.Find(t => t.Type == type);
            pages.ForEach(t => t.Init());
            await UniTask.Delay(100);
            currentPage.Show();
        }

        public void OpenPage(PageType type)
        {
            var page = pages.Find(t => t.Type == type);
            print("Open Page Type : " + type.ToString());
            if (page == null)
                return;

            print("Opened");

            if (currentPage == null)
                return;

            print("current page was => " + currentPage.transform.name);

            currentPage.Hide();
            previousPage = currentPage;
            openPages.Add(currentPage);
            currentPage = page;
            currentPage.Show();
        }

        public void ClosePage()
        {
            currentPage.Hide();
            if (openPages.Count > 0)
            {
                currentPage = openPages[^1];
                openPages.Remove(
                    openPages[^1]);
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