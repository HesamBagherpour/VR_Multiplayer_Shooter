using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Packages.UIController.Script.Tool
{
    public class UiMeshColliderHandler : MonoBehaviour
    {
        public async void Start()
        {
            await UniTask.Delay(200);
            MeshCollider meshCollider = GetComponent<MeshCollider>();
            if(!meshCollider)
                return;
            meshCollider.enabled = false;
        }
    }
}
