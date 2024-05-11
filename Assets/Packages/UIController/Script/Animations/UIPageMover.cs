using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Packages.UIController.Script.UI;

namespace Packages.UIController.Script.Animations
{
    public class UIPageMover : BaseAnimationUiPage
    {
        public override AnimationType Type => AnimationType.EndAnimation;

        public override void Show(BasePageUI page)
        {
            throw new System.NotImplementedException();
        }

        public override void Close(BasePageUI page)
        {
        }

        private void AnimationShowPageUI(BasePageUI page)
        {
            //page.root.transform.DOLocalMoveX(page.uiPageMover.pageEndPosition, page.uiPageMover.duration);
        }
    }
}