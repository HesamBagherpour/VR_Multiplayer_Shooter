using System;
using System.Collections.Generic;
using EnemyAI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using VR_PROJECT.Scripts.EnemyAI.ScriptableObjects;

namespace VR_PROJECT.Scripts.EnemyAI
{
    [RequireComponent(typeof(EnemyController))]
    public class EnemyManager : MonoBehaviour
    {
        private static EnemyManager instance;

        public static EnemyManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            Init();
        }

        public List<StateController> spawnedEnemies;
        public EnemyInfo enemies;
       private void Init()
        {
            if (enemies == null)
            {
                enemies = Resources.Load<EnemyInfo>("Enemy" + " " + (1));
            }
            GetComponent<EnemyController>().Spawner(enemies);
        }
             
            
        }
    }
