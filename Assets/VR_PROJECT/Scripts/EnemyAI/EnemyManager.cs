using System;
using System.Collections.Generic;
using EnemyAI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using VR_PROJECT.Scripts.EnemyAI.ScriptableObjects;

namespace VR_PROJECT.Scripts.EnemyAI
{
    public class EnemyManager : MonoBehaviour
    {
        public List<EnemyInfo> enemies;
        private void OnValidate()
        {
            if (enemies != null)
            {
                return;
            }
            // Iterate through the enemies list
            for (int i = 0; i < enemies.Count; i ++)
            {
                // If enemy is null or already exists, skip
                if (enemies[i] == null)
                    continue;

                // Create a new instance of the enemy ScriptableObject
                EnemyInfo newEnemy = CreateEnemyScriptableObject(enemies[i], i);

            }
        }
        private EnemyInfo CreateEnemyScriptableObject(EnemyInfo enemy, int number)
        {
            // Create a new instance of the enemy ScriptableObject
            EnemyInfo newEnemy = ScriptableObject.CreateInstance<EnemyInfo>();
            
            // Save the ScriptableObject asset to the Resources folder
            string path = "Assets/Resources/EnemyAI/ScriptableObjects" + "Enemy" + " " + number + ".asset";
            AssetDatabase.CreateAsset(newEnemy, path);
            AssetDatabase.SaveAssets();

            Debug.Log("Enemy ScriptableObject created: " + newEnemy.name);

            // Return the new enemy instance
            return newEnemy;
        }

        private void Start()
        {
            for (int i = 1; i < enemies.Count+1; i++)
            {
                EnemyInfo enemyInfo = Resources.Load<EnemyInfo>("EnemyAI/Enemy" + " " + i);
               var enemy = Instantiate(enemyInfo.enemies.enemyPrefab, enemyInfo.enemies.patrolWayPoints[0].position, Quaternion.identity);
               var state = enemy.GetComponent<StateController>();
               state.aimTarget = enemyInfo.enemies.aimTarget;
               state.patrolWayPoints = enemyInfo.enemies.patrolWayPoints;
               state.bullets = enemyInfo.enemies.bullets;
               state.viewRadius = enemyInfo.enemies.viewRadius;
               state.viewAngle = enemyInfo.enemies.viewAngle;
               state.perceptionRadius = enemyInfo.enemies.perceptionRadius;
            }
        }
    }
}