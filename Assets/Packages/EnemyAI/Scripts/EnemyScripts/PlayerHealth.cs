using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthManager
{
   private PlayerAssign playerAssign;
   public float health = 100;
   public bool isDummy;
   private void Awake()
   {
      if (isDummy)
      {
         dead = true;
         return;
      }
      playerAssign = GetComponent<PlayerAssign>();
   }

   public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart = null, GameObject origin = null)
   {
      if (isDummy)
      {
         //dead = true;
         return;
      }
      health -= damage;
      if (health <= 0)
      {
         dead = true;
         health = 0;
         Destroy(gameObject);
         Debug.Log("Dead");
         
         PlayerManager.Instance.OnPlayerDead(gameObject);
      }
      else
      {
         Debug.Log("health : " + health);
      }
   }
}
