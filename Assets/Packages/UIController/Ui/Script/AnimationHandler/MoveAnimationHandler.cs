using DG.Tweening;
using UnityEngine;

namespace Packages.UIController.AnimationHandler
{
    public class MoveAnimationHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform root;
        [SerializeField] private Vector3 moveIn;
        [SerializeField] private Vector3 moveOut;
        [SerializeField] private float duration;

        private void OnEnable()
        {
            root.DOLocalMove(moveIn, duration);
        }
    }
}