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
        [SerializeField] public ScaleType scaleType;

        public enum ScaleType
        {
            ScaleUp = 0,
            ScaleLeft = 1
        }

        private CurrentAnimationState CurrentAnimationStates { get; }
        public override CurrentAnimationState Type => state;
        public override ComponentType ComponentType => ComponentType.Scale;


        public override void Show()
        {
            AnimationScale();
            print("<color=yellow> close called </color>");
        }

        public override void Close()
        {
            AnimationScale();
        }

        private void AnimationScale()
        {
            print("<color=red> AnimationScale here </color>");
            var mySequence = DOTween.Sequence();


            if (ScaleType.ScaleUp == scaleType)
            {
                mySequence.Append(root.DOScale(target, duration));
                print(" isFinished : " + isFinished);

                //   mySequence.AppendCallback(() => ResetAnimation(page));
                mySequence.OnComplete(() => isFinished = true);
                print("Target : " + target);


                mySequence.Play();
                return;
            }

            mySequence = DOTween.Sequence();
            mySequence.Append(root.DOScale(target, duration));

            //   mySequence.AppendCallback(() => ResetAnimation(page));
            mySequence.OnComplete(() => isFinished = true);
            print("parent : " + root.parent.name);
            print("parent : " + scaleType);
            print("parent : " + Type);
            mySequence.Play();
        }
    }
}