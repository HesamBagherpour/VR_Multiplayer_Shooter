using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;
using VR_PROJECT.Scripts.EnemyAI;
using VR_PROJECT.Scripts.EnemyAI.ScriptableObjects;

public class EnemyController : MonoBehaviour
{
    public void Spawner(EnemyInfo enemies)
    {
         EnemyInfo enemyInfo = Resources.Load<EnemyInfo>("EnemyAI/Enemy" + " " + (1));
            for (int i = 0; i < enemies.enemies.Count; i++)
            {
                var enemy = Instantiate(enemies.enemy, 
                    enemies.enemies[i].patrolWayPoints[0].position, Quaternion.identity);
                var state = enemy.GetComponent<StateController>();
                EnemyManager.Instance.spawnedEnemies.Add(state);
                var classState = state.classStats; 
                //state.aimTarget = enemyInfo.enemies.aimTarget;
                state.patrolWayPoints = enemies.enemies[i].patrolWayPoints;
                state.bullets = enemies.enemies[i].bullets;
                state.viewRadius = enemies.enemies[i].viewRadius;
                state.viewAngle = enemies.enemies[i].viewAngle;
                state.perceptionRadius = enemies.enemies[i].perceptionRadius;
                state.classStats.bulletDamage = enemies.enemies[i].bulletDamage;
                state.classStats.changeCoverChance = enemies.enemies[i].changeCoverChance;
                state.classStats.weaponType = enemies.enemies[i].weaponType;
                state.classStats.shotSound = enemies.enemies[i].shotSound;
                state.classStats.reloadSound = enemies.enemies[i].reloadSound;
                state.classStats.muzzleFlash = enemies.enemies[i].muzzleFlash;
                state.classStats.sparks = enemies.enemies[i].sparks;
                state.classStats.shot = enemies.enemies[i].shot;
                state.classStats.bulletHole = enemies.enemies[i].bulletHole;
                state.classStats.shotErrorRate = enemies.enemies[i].shotErrorRate;
                state.classStats.shotRateFactor = enemies.enemies[i].shotRateFactor;

                state.generalStats.patrolSpeed = enemies.enemies[i].patrolSpeed;
                state.generalStats.chaseSpeed = enemies.enemies[i].chaseSpeed;
                state.generalStats.evadeSpeed = enemies.enemies[i].evadeSpeed;
                state.generalStats.patrolWaitTime = enemies.enemies[i].patrolWaitTime;
                state.generalStats.obstacleMask = enemies.enemies[i].obstacleMask;
                state.generalStats.angleDeadzone = enemies.enemies[i].angleDeadzone;
                state.generalStats.speedDampTime = enemies.enemies[i].speedDampTime;
                state.generalStats.angularSpeedDampTime = enemies.enemies[i].angularSpeedDampTime;
                state.generalStats.angleResponseTime = enemies.enemies[i].angularResponseTime;
                state.generalStats.aboveCoverHeight = enemies.enemies[i].aboveCoverHeight;
                state.generalStats.coverMask = enemies.enemies[i].coverMask;
                state.generalStats.shotMask = enemies.enemies[i].shotMask;
                state.generalStats.targetMask = enemies.enemies[i].targetMask;
            }
    }
}
