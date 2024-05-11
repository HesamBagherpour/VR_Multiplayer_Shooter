using Packages.UIController.Script.UI;
using UnityEngine;
using DG.Tweening;
namespace Packages.UIController.Script.Animations
{
    public class MoveAnimationHandler : BaseAnimationUIPage
    {
        
        [SerializeField] private int target;
        [SerializeField] private float duration;
        [SerializeField] public AnimationState state;
        [SerializeField] private MoveType moveType;
        public bool isFinished = false;

        public enum MoveType : byte
        {
            MoveUp = 0,
            MoveDown = 1
        }

        private  AnimationState animationStates { get; }
        public override AnimationState Type => state;

        public override void Show(PageType pageType)
        {
         
                AnimationMove(pageType);
            
        }

        public override void Close(PageType pageType)
        {

           
                AnimationMove(pageType);
            
        }

        private void AnimationMove(PageType pageType)
        {
            Debug.Log("open here");

            if ( MoveType.MoveUp==moveType)
            {
                Debug.Log("here is");
                var page =  UIManager.Instance.pages.Find(t => t.Type == pageType);
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(page.root.transform.DOLocalMoveX(target, duration));
                mySequence.OnComplete(() => isFinished = true);
                mySequence.Play();
            }
            else
            {
                var page =  UIManager.Instance.pages.Find(t => t.Type == pageType);
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(page.root.transform.DOLocalMoveY(target, duration));
                mySequence.OnComplete(() => isFinished = true);
                mySequence.Play();
            }
            
        }
    }
}