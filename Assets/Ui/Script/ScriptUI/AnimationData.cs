using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.ScriptUI;
using UnityEngine;

[CreateAssetMenu(fileName = "DataAnimation", menuName = "ScriptableObjects/DataAnimation")]
public class AnimationData : ScriptableObject
{
    public List<BaseAnimationUiPage> listAnimationData = new List<BaseAnimationUiPage>();

    public BaseAnimationUiPage GetAnimationType(AnimationType type)
    {
        var page = listAnimationData.FirstOrDefault(p => p.Type == type);
        if (page is null)
        {
            Debug.LogError($"There is no page with name {name}");
        }
        return page;
    }
   
  
}