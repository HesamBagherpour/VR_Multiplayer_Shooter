using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VR_PROJECT;
using VR_PROJECT.Network.Core;

public class FishNet_Test : MonoBehaviour
{
    [SerializeField] private Button _serverButton;
    [SerializeField] private Button _clientButton;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _dectiveColor;

    private INetworkController _networkController;

    private void Awake()
    {
        _serverButton.onClick.AddListener(ServerClick);
        _clientButton.onClick.AddListener(ClientClick);
    }

    private void Start()
    {
        _networkController = GameManager.Instance.NetworkController;
    }

    private void ServerClick()
    {
        _networkController.ConnectServer();
    }

    private void ClientClick()
    {
        _networkController.ConnectClient();
    }
}
