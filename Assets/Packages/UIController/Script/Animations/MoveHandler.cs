using Packages.UIController.Script.UI;
using UnityEngine;
using DG.Tweening;
using Packages.UIController.Script.Base;

namespace Packages.UIController.Script.Animations
{
    public class MoveHandler : AnimationBaseUI
    {
        [SerializeField] private int target;
        [SerializeField] private float duration;
        [SerializeField] public CurrentAnimationState state;
        [SerializeField] private MoveType moveType;

        public enum MoveType : byte
        {
            MoveUp = 0,
            MoveDown = 1
        }

        private CurrentAnimationState CurrentAnimationStates { get; }
        public override CurrentAnimationState Type => state;
        public override ComponentType ComponentType { get; }

        public override void Show()
        {
            AnimationMove();
        }

        public override void Close()
        {
            AnimationMove();
        }

        private void AnimationMove()
        {
            var mySequence = DOTween.Sequence();

            if (MoveType.MoveUp == moveType)
            {
                mySequence.Append(root.DOLocalMoveX(target, duration));
                mySequence.OnComplete(() => isFinished = true);
                mySequence.Play();
                return;
            }

            mySequence = DOTween.Sequence();
            mySequence.Append(root.DOLocalMoveY(target, duration));
            mySequence.OnComplete(() => isFinished = true);
            mySequence.Play();
        }
    }
}