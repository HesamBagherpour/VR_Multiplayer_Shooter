using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;

namespace Script.ScriptUI
{
    public abstract class BaseAnimationUiPage : MonoBehaviour
    {
        public abstract AnimationType Type { get; }
        public abstract void Show(BasePageUi page);
        public abstract void Close(BasePageUi page);

        public int pageStartPosition;

        [FormerlySerializedAs("PageEndPosition")]
        public int pageEndPosition;

        public float duration;
             
        public  void ResetAnimation(BasePageUi page)
        {
            var animType = UiManager.instance.animationUiPages.FirstOrDefault( p=> p.Type == AnimationType.ScaleUp);
            if (animType != null)
            {
                page.gameObject.transform.GetChild(0).gameObject.transform.DOScaleY(animType.pageStartPosition, animType.duration);
            }
            var animationUiPage = UiManager.instance.animationUiPages.FirstOrDefault( p=> p.Type == AnimationType.MoveLeft);
            if (animationUiPage != null)
            {
                page.gameObject.transform.GetChild(0).gameObject.transform.DOLocalMoveX(animationUiPage.pageStartPosition, animationUiPage.duration);
            }
        }

    }

   
}

public enum AnimationType
{
    None = 0,
    MoveUp = 1,
    MoveDown = 2,
    MoveLeft = 3,
    MoveRight = 4,
    ScaleUp = 5,
    ScaleDown = 6,
}