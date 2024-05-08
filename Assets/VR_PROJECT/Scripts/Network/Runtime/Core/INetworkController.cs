using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using VR_PROJECT.General;

namespace VR_PROJECT.Network.Core
{
    public interface INetworkController : IService<bool>
    {
        bool IsAvailable { get; }
        bool IsServerStarted { get; }
        bool IsClientStarted { get; }
        bool IsHostStarted { get; }
        bool IsOffline { get; }

        UniTask<Result> ConnectServer(ushort port = 0);
        UniTask<Result> DisconnectServer(bool sendDisconnetionMessage = false);
        UniTask<Result> ConnectClient(string address = "", ushort port = 0);
        UniTask<Result> DisconnectClient();

    }
}