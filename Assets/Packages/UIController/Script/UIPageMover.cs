using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.ScriptUI;
using UnityEngine;
using DG.Tweening;

public class UIPageMover : BaseAnimationUiPage
{
    public override AnimationType Type => AnimationType.EndAnimation;
    public override void Show(BasePageUI page)
    {
        AnimationShowPageUI(page);
    }
    public override void Close(BasePageUI page)
    {
    }
    private void AnimationShowPageUI(BasePageUI page )
    {
            page.root.transform.DOLocalMoveX(page.uiPageMover.pageEndPosition, page.uiPageMover.duration);
    }
}