using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthManager
{
   private PlayerAssign playerAssign;
   public float health = 100;

   private void Awake()
   {
      playerAssign = GetComponent<PlayerAssign>();
   }

   public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart = null, GameObject origin = null)
   {
      health -= damage;
      if (health <= 0)
      {
         dead = true;
         health = 0;
         gameObject.SetActive(false);

         Debug.Log("Dead");
         
         PlayerSpawnManager.Instance.OnPlayerDead(this.gameObject);
         //SceneManager.LoadScene(0);
      }
      else
      {
         Debug.Log("health : " + health);
      }
   }
}
