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
        var animType = UIManager.Instance.animationUiPages.FirstOrDefault(p => p.Type == AnimationType.ScaleUp);
        if (animType != null)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(page.gameObject.transform.GetChild(0)
                .DOScaleY(animType.pageEndPosition, animType.duration));
            mySequence.OnComplete(() => AnimationCompleteCallback(page));
            mySequence.AppendCallback(() => ResetAnimation(page));
            mySequence.Play();
        }
    }

    void MyCallback(BasePageUI page)
    {
        page.Hide(page);
    }


    void AnimationCompleteCallback(BasePageUI page)
    {
        Debug.Log("Animation completed!");
        page.Hide(page);
    }
}