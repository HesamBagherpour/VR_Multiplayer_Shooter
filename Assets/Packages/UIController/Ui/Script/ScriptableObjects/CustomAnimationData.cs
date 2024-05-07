using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Packages.UIController
{
    [CreateAssetMenu(fileName = "CustomDataAnimation", menuName = "ScriptableObjects/CustomDataAnimation")]
    public class CustomAnimationData : ScriptableObject
    {
        public List<MyAnimation> MyAnimations = new();

        public static CustomAnimationData Instance { private set; get; }

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDisable()
        {
            Instance = null;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }

    [System.Serializable]
    public class MyAnimation
    {
        public enum AnimationType : byte
        {
            MoveLeft = 1,
            MoveRight = 2,
            MoveUp = 3,
            MoveDown = 4,
            ScaleUp = 5,
            ScaleDown = 6,
        }

        public AnimationType animationType;
        public Vector3 dst;
        public float fadeAmount;
        public float dstScale;
        public float duration;
    }
}