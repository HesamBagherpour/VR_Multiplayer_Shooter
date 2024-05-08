using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Packages.UIController.AnimationHandler
{
    public class FadeAnimationHandler : MonoBehaviour
    {
        [SerializeField] private Image source;
        [SerializeField] private float fadeInAmount = 1;
        [SerializeField] private float fadeOutAmount = 0;
        [SerializeField] private float duration = 0.2f;

        public void OnEnable()
        {
            source.DOFade(fadeInAmount, duration);
        }
    }
}