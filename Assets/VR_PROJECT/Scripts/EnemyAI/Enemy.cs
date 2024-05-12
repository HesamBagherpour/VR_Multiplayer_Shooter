using System;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;
using UnityEngine.Serialization;

namespace VR_PROJECT.Scripts.EnemyAI
{
    [Serializable]
    public class Enemy
    {
        public StateController enemyPrefab;
        public StateController myState;
        public ClassStats myClass;
        public Transform aimTarget;
        public List<Transform> patrolWayPoints = new List<Transform>();
        public int bullets;
        public float viewRadius;
        public float viewAngle;
        public float perceptionRadius;
        [Tooltip("The value between 0 and 100.")]
        [Range(0f, 100f)]
        public int changeCoverChance;

        public ClassStats.WeaponType weaponType;
        public float bulletDamage;
        public AudioClip shotSound;
        public AudioClip reloadSound;
        public GameObject muzzleFlash, shot, sparks, bulletHole;
        public float shotRateFactor;
        public float shotErrorRate;

        public float patrolSpeed = 2;
        public float chaseSpeed = 5;
        public float evadeSpeed = 15;
        public float patrolWaitTime = 2;
        public LayerMask obstacleMask;
       public float angleDeadzone = 5;
        public float speedDampTime = 0.4f;
        public float angularSpeedDampTime = 0.2f;
        public float angularResponseTime = 2;
        public float aboveCoverHeight = 1.5f;
        public LayerMask coverMask;
        
        public LayerMask shotMask;
        public LayerMask targetMask;
    }
}