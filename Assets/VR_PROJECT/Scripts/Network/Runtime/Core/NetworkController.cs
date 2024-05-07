using System.Collections;
using System.Collections.Generic;
using FishNet.Managing;
using UnityEngine;

namespace VR_PROJECT.Network.Core
{
    public abstract class NetworkController : INetworkController
    {
        #region Field

        private NetworkConfiguration _configuration;
        protected bool _isAvailable;

        #endregion

        #region Properties

        public bool IsAvailable => _isAvailable;

        #endregion
        
        #region Initialize
        
        public virtual void Init()
        {
            _configuration = LoadConfiguration();
            
            if (!CanInit())
            {
                _isAvailable = false;
                return;
            }
        }
        
        #endregion

        #region Private Methods

        protected abstract NetworkConfiguration LoadConfiguration();
        protected virtual bool CanInit()
        {
            if (_configuration is null)
            {
                Debug.LogError("NetworkConfig is not loaded and Network not available");
                return false;
            }

            return true;
        }

        #endregion
    }
}