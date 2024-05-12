using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Emaj.Patterns;
using Packages.UIController.Script.Animations;
using Packages.UIController.Script.Base;
using Packages.UIController.Script.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;


namespace Packages.UIController.Script.UI
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [FormerlySerializedAs("currentPage")] public PageBaseUI current;
        public List<PageBaseUI> pages;

        [FormerlySerializedAs("firstPageUI")] [SerializeField]
        private PageBaseUI firstUI;

        [FormerlySerializedAs("previousPage")] public PageBaseUI previous;
        public List<PageBaseUI> openPages = new();


        [Header("TEMP PLAYER")] public Transform target;

        private void Awake()
        {
            Init(PageType.MainMenu);
        }

        private async void Init(PageType type)
        {
            current = firstUI = pages.Find(t => t.Type == type);
            openPages.Add(current);
            pages.ForEach(t => t.Init());
            //PopupUI.Instance.Init();
            await UniTask.Delay(100);
            current.Show();


            MainMenuUI.Instance.Init(target);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (MainMenuUI.Instance.IsActive())
                {
                    MainMenuUI.Instance.Hide();
                    return;
                }

                MainMenuUI.Instance.Show();
            }
        }

        public void OpenPage(PageType type)
        {
            var page = pages.Find(t => t.Type == type);
            if (page == null)
                return;
            if (current == null)
                return;
            current.Hide();
            previous = current;
            current = page;
            openPages.Add(current);

            current.Show();

            Debug.Log("open");
        }

        public void ClosePage()
        {
            Debug.Log("// " + previous);

            current.Hide();
            print("CURRENT BEFORE " + current.rootCanvas.transform.name);

            if (openPages.Count > 0)
            {
                openPages.Remove(openPages[^1]);
                previous = openPages[^1];
            }

            current = previous;

            print("CURRENT AFTER " + current.rootCanvas.transform.name);
            current.Show();
        }

        public void CloseAllPages()
        {
            current.root.SetActive(false);
            current = null;
            openPages.Clear();
        }
    }
}