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
        public abstract void Show(BasePageUI page);
        public abstract void Close(BasePageUI page);

        public int pageStartPosition;

        [FormerlySerializedAs("PageEndPosition")]
        public int pageEndPosition;

        public float duration;

        public void ResetAnimation(BasePageUI page)
        {
            if (page.uiPageScaler != null)
            {
                page.root.transform.DOScaleY(1, page.uiPageScaler.duration);
            }

            if (page.uiPageMover != null)
            {
                page.root.transform.DOMoveX(page.uiPageMover.pageStartPosition, page.uiPageMover.duration);
            }

        }
    }

    public enum AnimationType
    {
        None = 0,
        StartAnimation = 1,
        EndAnimation = 2,
    }
}