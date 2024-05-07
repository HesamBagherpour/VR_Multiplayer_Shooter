using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.ScriptUI;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIPageScaler : BaseAnimationUiPage
{
    public override AnimationType Type => AnimationType.ScaleUp;

    public override void Show(BasePageUI page)
    {
    }

    public override void Close(BasePageUI page)
    {
        CLosePageUIScaleY(page);
    }

    private void CLosePageUIScaleY(BasePageUI page)
    {
        var animType = UIManager.instance.animationUiPages.FirstOrDefault(p => p.Type == AnimationType.ScaleUp);
        if (animType != null)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(page.root.transform.DOScaleY(animType.pageEndPosition, animType.duration));
            mySequence.OnComplete(() => AnimationCompleteCallback(page));
            mySequence.AppendCallback(() => ResetAnimation(page));
            mySequence.Play();
        }
    }


    void AnimationCompleteCallback(BasePageUI page)
    {
        Debug.Log("Animation completed!");
        page.Hide(page);
    }
}