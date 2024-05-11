using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Packages.UIController.Script.UI;


namespace Packages.UIController.Script.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PageUI")]
    public class PageContainer : ScriptableObject
    {
        public List<BasePageUI> listPageUi = new List<BasePageUI>();

        public BasePageUI GetPage(PageType type)
        {
            var page = listPageUi.FirstOrDefault(p => p.Type == type);

            if (page is null)
            {
                Debug.LogError($"There is no page with name {name}");
            }

            return page;
        }
    }
}