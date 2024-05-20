using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;
using Packages.UIController.Script.UI;

namespace Packages.UIController.Script.Base
{
    public abstract class AnimationBaseUI : MonoBehaviour
    {
        public RectTransform root;
        public abstract CurrentAnimationState Type { get; }
        public abstract ComponentType ComponentType { get; }
        public abstract void Show();
        public abstract void Close();
        public bool isFinished = false;
        
    }

    [Serializable]
    public enum ComponentType : byte
    {
        Move = 0,
        Scale = 1,
        Fade = 2
    }

    public enum CurrentAnimationState : byte
    {
        None = 0,
        StartAnimation = 1,
        EndAnimation = 2
    }
}