using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;
using Packages.UIController.Script.UI;

namespace Packages.UIController.Script.Animations
{
    public abstract class BaseAnimationUIPage : MonoBehaviour
    {
        public abstract AnimationState Type { get; }
        public abstract void Show(PageType type);
        public abstract void Close(PageType type);
    }

    public enum AnimationState
    {
        None = 0,
        StartAnimation = 1,
        EndAnimation = 2,
    }
}