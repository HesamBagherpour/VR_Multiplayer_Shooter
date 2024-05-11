using FishNet.Managing;
using UnityEngine;
using VR_PROJECT.General;
using VR_PROJECT.Network.Core;

namespace VR_PROJECT.Network.Modules.FishNet
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = MenuPath, fileName = FileName)]
#endif
    public class FishNetConfiguration : NetworkConfiguration
    {
#if UNITY_EDITOR
        private const string MenuPath = Consts.MenuPath + "Netwok/Configurations/Create NetworkConfiguration";
        private const string FileName = nameof(NetworkConfiguration);
#endif
        
        [SerializeField] private NetworkManager _networkManager;

        public NetworkManager NetworkManager => _networkManager;
    }
}