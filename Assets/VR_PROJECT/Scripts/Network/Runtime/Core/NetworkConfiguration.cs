using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;
using VR_PROJECT.Common;

namespace VR_PROJECT.Network.Core
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = MenuPath, fileName = FileName)]
#endif
    public class NetworkConfiguration : ScriptableObject
    {
#if UNITY_EDITOR
        
        private const string MenuPath = Consts.MenuPath + "Netwok/Create NetworkConfiguration";
        private const string FileName = nameof(NetworkConfiguration);
#endif
        [SerializeField] private NetworkManager _networkManager;
        [SerializeField] private IPAddressType _ipType;
        [SerializeField] private string _ip;
        [SerializeField] private ushort _port;

        public NetworkManager NetworkManager => _networkManager;
        public IPAddressType IPType => _ipType;
        public string IP => _ip;
        public ushort Port => _port;
    }
    
}

