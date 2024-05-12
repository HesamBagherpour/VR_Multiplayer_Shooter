using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace VR_PROJECT.Scripts.EnemyAI
{
    public class PlayerController : MonoBehaviour
    {
        public void Spawner(GameObject player)
        {
            var newPlayer = Instantiate(player, new Vector3(11.5f, 0, -7.5f), quaternion.identity);
            newPlayer.GetComponent<PlayerHealth>().isDummy = true;
        }

        public void InitNewPlayer(GameObject player)
        {
            StartCoroutine(InitNewPlayerCoroutine(player));
        }

        private IEnumerator InitNewPlayerCoroutine(GameObject player)
        {
            yield return new WaitForSeconds(5f);
            var newPlayer = Instantiate(player, new Vector3(11.5f, 0, -7.5f), quaternion.identity);
        }
    }
}