using System.Collections;
using System.Collections.Generic;
using Packages.UIController;
using Packages.UIController.Utils;
using Script.ScriptUI;
using UnityEngine;

public class TempUI : MonoBehaviour
{
    [SerializeField] private RectTransform root;

    public void Start()
    {
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            root.DoCustomMove(
                CustomAnimationData.Instance.MyAnimations.Find(t =>
                    t.animationType == MyAnimation.AnimationType.MoveLeft), false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            root.DoCustomMove(
                CustomAnimationData.Instance.MyAnimations.Find(t =>
                    t.animationType == MyAnimation.AnimationType.MoveRight));
        }
    }
}