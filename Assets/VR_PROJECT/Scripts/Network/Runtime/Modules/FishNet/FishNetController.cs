using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FishNet.Managing;
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

            if (result != null)
                return result;

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

        #region Private Methods

        protected override NetworkConfiguration LoadConfiguration()
        {
            _fishNetConfiguration = Resources.Load<FishNetConfiguration>(nameof(FishNetConfiguration));
            return _fishNetConfiguration;
        }

        protected override async UniTask<Result<bool>> CanInit()
        {
            var result = await base.CanInit();

            if (result != null)
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