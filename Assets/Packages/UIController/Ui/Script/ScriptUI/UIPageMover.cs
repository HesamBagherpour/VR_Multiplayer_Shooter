using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.ScriptUI;
using UnityEngine;
using DG.Tweening;

public class UIPageMover : BaseAnimationUiPage
{

    public override AnimationType Type => AnimationType.MoveLeft;
    
    public override void Show(BasePageUI page)
    {
        AnimationShowPageUI(page);
    }

    public override void Close(BasePageUI page)
    {
    }
    private void AnimationShowPageUI(BasePageUI page )
    {
        var animType = UIManager.instance.animationUiPages.FirstOrDefault( p=> p.Type == AnimationType.MoveLeft);
        Debug.Log("here 12");
        if (animType!=null)
        {
            Debug.Log("here 13");
            page.gameObject.transform.GetChild(0).gameObject.transform.DOLocalMoveX(animType.pageEndPosition, animType.duration);
        }
    }
}