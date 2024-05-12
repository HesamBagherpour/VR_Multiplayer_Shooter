using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;

public class PlayerRefrenceFinder : MonoBehaviour
{
   [SerializeField]private StateController stateController;

   private void OnEnable()
   {
      AddDummy();
   }

   public void AddDummy()
   {
      if (stateController == null)
      {
         stateController = GetComponent<StateController>();
      }
      var dummy = GameObject.FindWithTag("dummy");
      if (dummy == null)
      {
         dummy = new GameObject();
         dummy.AddComponent<PlayerHealth>().dead = true;
         dummy.tag = "dummy";
         dummy.layer = LayerMask.NameToLayer("Default");
      }
     
      stateController.aimTarget = dummy.transform;
      //dummy.SetActive(false);
   }
}
