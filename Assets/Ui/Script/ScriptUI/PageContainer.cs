using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.ScriptUI;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PageUI")]

public class PageContainer : ScriptableObject
{
    public List<BasePageUi> listPageUi = new List<BasePageUi>();
    public BasePageUi GetPage(PageType type)
    {
        var page = listPageUi.FirstOrDefault(p => p.Type == type);

        if (page is null)
        {
            Debug.LogError($"There is no page with name {name}");
        }

        return page;
    }

}
