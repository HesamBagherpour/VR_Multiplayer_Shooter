using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.ScriptUI;
using UnityEngine;
using DG.Tweening;

public class UIPageMover : BaseAnimationUiPage
{

    public override AnimationType Type => AnimationType.MoveLeft;
    
    public override void Show(BasePageUi page)
    {
        AnimationShowPageUI(page);
    }

    public override void Close(BasePageUi page)
    {
    }
    private void AnimationShowPageUI(BasePageUi page )
    {
        var animType = UiManager.instance.animationUiPages.FirstOrDefault( p=> p.Type == AnimationType.MoveRight);
        if (animType!=null)
        {
            Debug.Log("here 11");
            page.gameObject.transform.GetChild(0).gameObject.transform.DOLocalMoveX(animType.pageEndPosition, animType.duration);
        }
    }
}