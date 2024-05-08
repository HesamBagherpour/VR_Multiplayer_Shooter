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
    public override AnimationType Type => AnimationType.EndAnimation;
    public AnimationType animationType;
    public override void Show(BasePageUI page)
    {
        CLosePageUIScale(page);
    }

    public override void Close(BasePageUI page)
    {
       
    }

    private void CLosePageUIScale(BasePageUI page)
    {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(page.root.transform.DOScaleY(page.uiPageScaler.pageEndPosition, page.uiPageScaler.duration));
            mySequence.OnComplete(() => AnimationCompleteCallback(page));
            mySequence.AppendCallback(() => ResetAnimation(page));
            mySequence.Play();
    }


    void AnimationCompleteCallback(BasePageUI page)
    {
        Debug.Log("Animation completed!");
        page.Hide(page);
    }
}