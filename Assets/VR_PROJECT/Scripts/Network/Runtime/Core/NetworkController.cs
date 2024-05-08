using System.Collections;
using System.Collections.Generic;
using FishNet.Managing;
using UnityEngine;

namespace VR_PROJECT.Network.Core
{
    public class NetworkController : INetworkController
    {
        #region Field

        private NetworkConfiguration _configuration;
        private bool _isAvailable;

        #endregion

        #region Properties

        public bool IsAvailable => _isAvailable;

        #endregion
        
        #region CTORs

        public NetworkController()
        {
            _configuration = Resources.Load<NetworkConfiguration>(nameof(NetworkConfiguration));
        }
        
        #endregion
        
        #region Initialize
        
        public void Init()
        {
            if (!IsConfigValid())
            {
                _isAvailable = false;
                return;
            }

            var networkManager = GameObject.Instantiate(_configuration.NetworkManager);
        }

        #endregion
        
        #region Public Methods

        

        #endregion

        #region Private Methods

        private bool IsConfigValid()
        {
            if (_configuration is null)
            {
                Debug.LogError("NetworkConfig is not loaded and Network not available");
                return false;
            }

            if (_configuration.NetworkManager is null)
            {
                Debug.LogError("NetworkManager in NetworkConfig is null pls insure to fill it properly");
                return false;
            }

            return true;
        }

        #endregion
    }
}