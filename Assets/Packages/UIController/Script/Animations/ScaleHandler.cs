using Packages.UIController.Script.UI;
using UnityEngine;
using DG.Tweening;
using Packages.UIController.Script.Base;

namespace Packages.UIController.Script.Animations
{
    public class ScaleHandler : AnimationBaseUI
    {
        [SerializeField] private int target;
        [SerializeField] private float duration;
        [SerializeField] public CurrentAnimationState state;
        [SerializeField] private ScaleType scaleType;

        public enum ScaleType
        {
            ScaleUp = 0,
            ScaleLeft = 1
        }

        private CurrentAnimationState CurrentAnimationStates { get; }
        public override CurrentAnimationState Type => state;
        public override ComponentType ComponentType { get; }

        public override void Show()
        {
            print("<color=cyan> Show called </color>");
            AnimationScale();
        }

        public override void Close()
        {
            print("<color=cyan> close called </color>");
            AnimationScale();
        }

        private void AnimationScale()
        {
            print("parent : " + root.parent.name);
            print("Target : " + target);
            print("type : " + Type);
            var mySequence = DOTween.Sequence();
            if (ScaleType.ScaleUp == scaleType)
            {
                mySequence.Append(root.DOScaleX(target, duration));
                //   mySequence.AppendCallback(() => ResetAnimation(page));
                mySequence.OnComplete(() => isFinished = true);
                mySequence.Play();
                return;
            }

            mySequence = DOTween.Sequence();
            mySequence.Append(root.DOScaleX(target, duration));
            //   mySequence.AppendCallback(() => ResetAnimation(page));
            mySequence.OnComplete(() => isFinished = true);
            mySequence.Play();
        }
    }
}