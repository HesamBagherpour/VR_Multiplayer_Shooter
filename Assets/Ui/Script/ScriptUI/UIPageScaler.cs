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

    public override void Show(BasePageUi page)
    {
        
    }

    public override void Close(BasePageUi page)
    {
        CLosePageUIScaleY(page);
    }
    private void CLosePageUIScaleY(BasePageUi page)
    {

        Debug.Log("this2");
      
            var animType = UiManager.instance.animationUiPages.FirstOrDefault( p=> p.Type == AnimationType.MoveLeft);
            if (animType!=null)
            {
                Sequence mySequence = DOTween.Sequence();

                // Append scale animation to the sequence
                mySequence.Append(page.gameObject.transform.GetChild(0).DOScaleY(animType.pageEndPosition, animType.duration));

                // Add a callback to execute when the animation completes
                mySequence.OnComplete(() => AnimationCompleteCallback(page));

                // Append a callback to execute after the animation
                mySequence.AppendCallback(() => ResetAnimation(page));

                // Play the sequence
                mySequence.Play();
            }

            
         
            Debug.Log("Scale Complete");
        
    }
    void MyCallback(BasePageUi page)
    {
        page.Hide(page);
    }
   

// Method to execute when the animation completes
void AnimationCompleteCallback(BasePageUi page)
{
    Debug.Log("Animation completed!");
    page.Hide(page);
}

// Method to reset the animation

}