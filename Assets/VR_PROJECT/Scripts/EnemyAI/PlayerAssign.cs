using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;

public class PlayerAssign : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private List<StateController> enemies;
    private void OnEnable()
    {
        enemies = new List<StateController>(FindObjectsOfType<StateController>());
        AssignPlayer();

    }

    private void AssignPlayer()
    {
        foreach (var enemy in enemies)
        {
            enemy.aimTarget = GetComponent<ChestHolderRefrence>().upperChest.transform;
        }
    }
}
