using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VR_PROJECT.Scripts.EnemyAI
{
    public class PlayerController : MonoBehaviour
    {
        public void Spawner(GameObject player, List<GameObject> spawnPoints)
        {
            int random = Random.Range(0, spawnPoints.Count);
            var newPlayer = Instantiate(player, spawnPoints[random].transform.position, quaternion.identity);
            //newPlayer.GetComponent<PlayerHealth>().isDummy = true;
        }

        public void InitNewPlayer(GameObject player, List<GameObject> spawnPoints)
        {
            StartCoroutine(InitNewPlayerCoroutine(player, spawnPoints));
        }

        private IEnumerator InitNewPlayerCoroutine(GameObject player, List<GameObject> spawnPoints)
        {
            yield return new WaitForSeconds(5f);
            //var newPlayer = Instantiate(player, new Vector3(11.5f, 0, -7.5f), quaternion.identity);
            Spawner(player, spawnPoints);
        }
    }
}