using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using VR_PROJECT.Scripts.EnemyAI;
using Action = System.Action;
[RequireComponent(typeof(PlayerController))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] float spawnTime= 3;
    public GameObject player;
    public bool hasInit;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private StateController enemy;
    private static PlayerManager instance;

    public static PlayerManager Instance
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
        if (!hasInit)
            return;
        Init();
    }

    private void Init()
    {
        if (spawnPoints.Count == 0)
        {
            spawnPoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("SpawnPoints"));
        }
        GetComponent<PlayerController>().Spawner(player, spawnPoints);
    }
    public void OnPlayerDead(GameObject destroyedPlayer)
    {
        for (int i = 0; i < EnemyManager.Instance.spawnedEnemies.Count; i++)
        { 
            EnemyManager.Instance.spawnedEnemies[i]
                .GetComponent<PlayerRefrenceFinder>().AddDummy();
            EnemyManager.Instance.spawnedEnemies[i].GetComponent<StateController>().Aiming = false;
        }
        GetComponent<PlayerController>().InitNewPlayer(player, spawnPoints);
    }
    
}
