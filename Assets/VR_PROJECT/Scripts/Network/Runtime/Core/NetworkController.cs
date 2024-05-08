using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VR_PROJECT.General;

namespace VR_PROJECT.Network.Core
{
    public abstract class NetworkController : INetworkController
    {
        #region Field

        private NetworkConfiguration _configuration;

        #endregion

        #region Properties

        public bool IsAvailable { get; protected set; }
        public bool IsServerStarted { get; protected set; }
        public bool IsClientStarted { get; protected set; }
        public bool IsHostStarted => IsServerStarted && IsClientStarted;
        public bool IsOffline => !IsServerStarted && !IsClientStarted;

        #endregion
        
        #region Initialize
        
        public virtual UniTask<Result<bool>> Init()
        {
            Result<bool> result = null;
            
            _configuration = LoadConfiguration();

            return CanInit();
        }
        
        #endregion

        #region INetworkController

        public abstract UniTask<Result> ConnectServer(ushort port = 0);
        public abstract UniTask<Result> DisconnectServer(bool sendDisconnetionMessage = false);
        public abstract UniTask<Result> ConnectClient(string address = "", ushort port = 0);
        public abstract UniTask<Result> DisconnectClient();

        #endregion

        #region Private Methods

        protected abstract NetworkConfiguration LoadConfiguration();
        protected virtual UniTask<Result<bool>> CanInit()
        {
            Result<bool> result = null;

            if (_configuration is null)
            {
                string errorMessage = "NetworkConfig is not loaded and Network not available";
                
                Debug.LogError(errorMessage);
                IsAvailable = false;
                
                result = new Result<bool>()
                {
                    IsSuccess = false,
                    Data = false,
                    ErrorMessage = errorMessage
                };
            }

            return UniTask.FromResult(result);
        }

        #endregion
    }
}