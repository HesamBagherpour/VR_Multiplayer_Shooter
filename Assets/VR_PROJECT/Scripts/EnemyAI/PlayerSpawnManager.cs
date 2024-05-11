using System;
using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private StateController enemy;
    
    private static PlayerSpawnManager instance;

    public static PlayerSpawnManager Instance
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

    public void OnPlayerDead(GameObject destroyedPlayer)
    {
        //Destroy(destroyedPlayer);
        destroyedPlayer.SetActive(false);
        StartCoroutine(InitNewPlayer());
    }

    private IEnumerator InitNewPlayer()
    {
        yield return new WaitForSeconds(5f);
        var newPlayer = Instantiate(player, new Vector3(11.5f, 0, -7.5f), quaternion.identity);
    }
}
