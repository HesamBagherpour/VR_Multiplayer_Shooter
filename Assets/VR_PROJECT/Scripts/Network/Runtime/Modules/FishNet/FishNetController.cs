using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FishNet.Managing;
using Unity.VisualScripting;
using UnityEngine;
using VR_PROJECT.General;
using VR_PROJECT.Network.Core;

namespace VR_PROJECT.Network.Modules.FishNet
{
    public class FishNetNetworkController : NetworkController
    {
        private FishNetConfiguration _fishNetConfiguration;
        private NetworkManager _networkManager;

        #region Initialize

        public override async UniTask<Result<bool>> Init()
        {
            var result = await base.Init();

            if (result is not null)
                return result;

            _networkManager = GameObject.FindObjectOfType<NetworkManager>();

            if (_networkManager is null)
                _networkManager = GameObject.Instantiate(_fishNetConfiguration.NetworkManager);

            if (!_networkManager.Initialized)
            {
                string errorMessage = "FishNet NetworkManager Not Initialized!!!";

                IsAvailable = false;

                Debug.LogError(errorMessage);

                result = new Result<bool>()
                {
                    IsSuccess = false,
                    Data = false,
                    ErrorMessage = errorMessage
                };

                return await UniTask.FromResult(result);
            }

            _networkManager.TransportManager.Transport.SetClientAddress(_fishNetConfiguration.Address);

            result = new Result<bool>()
            {
                IsSuccess = true,
                Data = true
            };

            return await UniTask.FromResult(result);
        }

        #endregion

        #region INetworkController

        public override UniTask<Result> ConnectServer(ushort port = 0)
        {
            if(port != 0)
                _networkManager.TransportManager.Transport.SetPort(port);
            
           var isConnected = _networkManager.ServerManager.StartConnection();

           var result = new Result()  { IsSuccess = isConnected };

           return UniTask.FromResult(result);
        }

        public override UniTask<Result> DisconnectServer(bool sendDisconnetionMessage = false)
        {
            var isDisconnected = _networkManager.ServerManager.StopConnection(sendDisconnetionMessage);
            
            var result = new Result()  { IsSuccess = isDisconnected };

            return UniTask.FromResult(result);
        }

        public override UniTask<Result> ConnectClient(string address = "", ushort port = 0)
        {
            if(address != string.Empty)
                _networkManager.TransportManager.Transport.SetClientAddress(address);
            
            if(port != 0)
                _networkManager.TransportManager.Transport.SetPort(port);

            var isConnected = _networkManager.ClientManager.StartConnection();

            var result = new Result()  { IsSuccess = isConnected };

            return UniTask.FromResult(result);
            
        }
        public override UniTask<Result> DisconnectClient()
        {
            var isDisconnected = _networkManager.ClientManager.StopConnection();
            
            var result = new Result()  { IsSuccess = isDisconnected };

            return UniTask.FromResult(result);
        }

        #endregion

        #region Private Methods

        protected override NetworkConfiguration LoadConfiguration()
        {
            _fishNetConfiguration = Resources.Load<FishNetConfiguration>(nameof(FishNetConfiguration));
            return _fishNetConfiguration;
        }

        protected override async UniTask<Result<bool>> CanInit()
        {
            var result = await base.CanInit();

            if (result is not null)
                return result;

            if (_fishNetConfiguration.NetworkManager is null)
            {
                string errorMessage = "FishNet NetworkManager is null in FishNetConfiguration!!!";

                Debug.LogError(errorMessage);
                IsAvailable = false;

                result = new Result<bool>()
                {
                    IsSuccess = false,
                    Data = false,
                    ErrorMessage = errorMessage
                };
            }

            return await UniTask.FromResult(result);
        }

        #endregion
    }
}