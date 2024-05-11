using System;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;

namespace VR_PROJECT.Scripts.EnemyAI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "newEnemy", menuName = "Enemies", order = 0)]
    [Serializable]
    public class EnemyInfo : ScriptableObject
    {
        public GameObject enemy;
        public Enemy enemies; 
        public ClassStats enemyClass;
        public StateController enemyStateController;
        
    }
}