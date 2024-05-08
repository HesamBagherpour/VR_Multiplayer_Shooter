using Cysharp.Threading.Tasks;
using Emaj.Patterns;
using UnityEngine;
using VR_PROJECT.Network.Core;
using VR_PROJECT.Network.Modules.FishNet;

namespace VR_PROJECT
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        #region Fields

        private NetworkController _networkController;
        
        #endregion

        #region Properties

        public INetworkController NetworkController => _networkController;

        #endregion

        private void Awake()
        {
            _networkController = new FishNetNetworkController();
        }

        private async void Start()
        {
            await Init();
        }

        // Start is called before the first frame update
        private async UniTask Init()
        {
           await _networkController.Init();
        }

        // Update is called once per frame
        private void Update()
        {
            
        }
    }
}