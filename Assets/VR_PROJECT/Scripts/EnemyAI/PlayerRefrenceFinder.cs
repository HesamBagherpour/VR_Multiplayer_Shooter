using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;

public class PlayerRefrenceFinder : MonoBehaviour
{
   [SerializeField]private StateController stateController;

   private void Awake()
   {
      if (stateController.aimTarget != null)
      {
         return;
      }
      stateController = GetComponent<StateController>();
   }
}
