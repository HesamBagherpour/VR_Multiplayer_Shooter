using System;
using EnemyAI;
using UnityEngine;

namespace Packages.EnemyAI.Scripts
{
    public class Alarm : MonoBehaviour
    {
        [SerializeField] private StateController stateController;

        private void Awake()
        {
            if (stateController == null)
            {
                stateController = GetComponent<StateController>();
            }
        }

        private void Update()
        {
            if (stateController.Aiming)
            {
                FindObjectOfType<AlertManagement>().RootAlertNearby(transform.position);
            }
        }
    }
}