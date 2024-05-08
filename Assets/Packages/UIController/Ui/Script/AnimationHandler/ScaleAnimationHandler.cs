using DG.Tweening;
using UnityEngine;

namespace Packages.UIController.AnimationHandler
{
    public class ScaleAnimationHandler : MonoBehaviour
    {
        [SerializeField] private RectTransform root;
        [SerializeField] private float scaleUpTo = 1;
        [SerializeField] private float scaleDownTo = 0;
        [SerializeField] private float duration = 0.2f;

        private void OnEnable()
        {
            root.DOScaleY(scaleDownTo, duration);
        }
    }
}