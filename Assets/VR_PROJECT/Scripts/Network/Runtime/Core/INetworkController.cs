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
    }
}