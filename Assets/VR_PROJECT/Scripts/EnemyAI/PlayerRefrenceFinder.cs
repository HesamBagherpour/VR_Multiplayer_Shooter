using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;

public class PlayerRefrenceFinder : MonoBehaviour
{
   private StateController stateController;

   private void Awake()
   {
      stateController = GetComponent<StateController>();
      //stateController.aimTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<ChestHolderRefrence>().upperChest.transform;
   }
}
