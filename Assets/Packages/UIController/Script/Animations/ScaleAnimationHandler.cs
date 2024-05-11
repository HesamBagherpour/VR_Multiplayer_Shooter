using Packages.UIController.Script.UI;
using UnityEngine;
using DG.Tweening;

namespace Packages.UIController.Script.Animations
{
    public class ScaleAnimationHandler : BaseAnimationUIPage
    {

        [SerializeField] private int target;
        [SerializeField] private float duration;
        [SerializeField] public AnimationState state;
        [SerializeField] private ScaleType scaleType;
        public bool isFinished = false;

        public enum ScaleType 
        {
            ScaleUp = 0,
            ScaleLeft = 1
        }

        private  AnimationState animationStates { get; }
        public override AnimationState Type => state;

        public override void Show(PageType pageType)
        {

            if (state != AnimationState.StartAnimation)
            {
                AnimationScale(pageType);
            }
        }

        public override void Close(PageType pageType)
        {
            Debug.Log("here is 1");

            if (state != AnimationState.EndAnimation)
            {
                Debug.Log("here is 11");

                AnimationScale(pageType);
            }
        }

        private void AnimationScale(PageType pageType)
        {
            if (ScaleType.ScaleUp==scaleType)
            {
                var page =  UIManager.Instance.pages.Find(t => t.Type == pageType);
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(page.root.transform.DOScaleX(target, duration));
                //   mySequence.AppendCallback(() => ResetAnimation(page));
                mySequence.OnComplete(() => isFinished = true);
                mySequence.Play();
            }
            else
            {
                var page =  UIManager.Instance.pages.Find(t => t.Type == pageType);
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(page.root.transform.DOScaleX(target, duration));
                Debug.Log("here is111"+target);

                //   mySequence.AppendCallback(() => ResetAnimation(page));
                mySequence.OnComplete(() =>isFinished = true);
                mySequence.Play();
            }
            
        }
    }
}