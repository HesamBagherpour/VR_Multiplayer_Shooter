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

        void SetBullets(int bullets)
        {
            this.bullets = bullets;
        }
    }
}