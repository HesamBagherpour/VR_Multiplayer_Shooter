using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;

public class PlayerRefrenceFinder : MonoBehaviour
{
   [SerializeField]private StateController stateController;

   private void Start()
   {
      stateController = GetComponent<StateController>();
      if (stateController.aimTarget != null)
      {
         return;
      }
      stateController.aimTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<ChestHolderRefrence>().upperChest.transform/*<ChestHolderRefrence>().upperChest.transform*/;
   }
}
