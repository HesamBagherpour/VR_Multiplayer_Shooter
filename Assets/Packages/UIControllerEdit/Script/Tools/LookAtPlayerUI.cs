using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerUI : MonoBehaviour
{
    public Transform player;

    public void Init(Transform target)
    {
        player = target;
    }
    
    public void LateUpdate()
    {
        if(!player)
            return;
        transform.LookAt(player);
    }
}
