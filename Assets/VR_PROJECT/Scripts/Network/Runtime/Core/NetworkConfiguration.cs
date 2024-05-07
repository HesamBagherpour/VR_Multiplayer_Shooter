using UnityEngine;
using UnityEngine.Serialization;
using VR_PROJECT.Common;

namespace VR_PROJECT.Network.Core
{

    public class NetworkConfiguration : ScriptableObject
    {
        [SerializeField] private string _address;
        [SerializeField] private ushort _port;

        public string Address => _address;
        public ushort Port => _port;
    }
    
}

