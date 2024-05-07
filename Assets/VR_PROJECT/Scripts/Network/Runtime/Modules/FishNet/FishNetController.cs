using FishNet.Managing;
using UnityEngine;
using VR_PROJECT.Network.Core;

namespace VR_PROJECT.Network.Modules.FishNet
{
    public class FishNetNetworkController : NetworkController
    {
        private FishNetConfiguration _fishNetConfiguration;
        private NetworkManager _networkManager;

        #region Initialize

        public override void Init()
        {
            base.Init();

            var networkManager = GameObject.Instantiate(_fishNetConfiguration.NetworkManager);

            if (!networkManager.Initialized)
            {
                _isAvailable = false;
                Debug.LogError("FishNet NetworkManager Not Initialized!!!");
                return;
            }
            
            networkManager.TransportManager.Transport.SetClientAddress(_fishNetConfiguration.Address);
        }

        #endregion

        #region Private Methods

        protected override NetworkConfiguration LoadConfiguration()
        {
            _fishNetConfiguration = Resources.Load<FishNetConfiguration>(nameof(FishNetConfiguration));
            return _fishNetConfiguration;
        }

        protected override bool CanInit()
        {
            if (!base.CanInit())
                return false;

            if (_fishNetConfiguration.NetworkManager is null)
            {
                Debug.LogError("FishNet NetworkManager is null in FishNetConfiguration!!!");
                return false;
            }

            return true;
        }

        #endregion
    }
}