using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.UIController.Utils
{
    public static class AnimationHandler
    {
        public static void DoCustomMove(this Transform target, MyAnimation myAnimation, bool doFull = true)
        {
            var dst = myAnimation.dst;
            var duration = myAnimation.duration;
            if (doFull)
            {
                target.DOMove(dst, duration);
                return;
            }

            var x = dst.x == 0 ? target.position.x : dst.x;
            var y = dst.y == 0 ? target.position.y : dst.y;
            var newDst = new Vector3(x, y, 0);
            target.DOMove(newDst, duration);
        }

        public static void DoCustomFade(this Image target, MyAnimation myAnimation)
        {
            var fadeAmount = myAnimation.fadeAmount;
            var duration = myAnimation.duration;
            target.DOFade(fadeAmount, duration);
        }

        public static void DoCustomScale(this Transform target, MyAnimation myAnimation)
        {
            var dstScale = myAnimation.dstScale;
            var duration = myAnimation.duration;
            target.DOScale(dstScale, duration);
        }
    }
}