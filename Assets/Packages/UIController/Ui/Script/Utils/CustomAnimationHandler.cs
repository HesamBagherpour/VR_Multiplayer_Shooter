using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public partial class CustomAnimationHandler : MonoBehaviour
{
    [SerializeField] private RectTransform root;
    [SerializeField] private Vector3 dst;
    [SerializeField] private float fadeAmount;
    [SerializeField] private float dstScale;
    [SerializeField] private float duration;

    public void Init()
    {
    }

    public void DoMove()
    {
        root.DOMove(dst, duration);
    }

    public void OnDoScale()
    {
        root.DOScale(dstScale, duration);
    }
}