using VR_PROJECT.General;

namespace VR_PROJECT.Inputs
{
    public interface IInputManager : IService<bool>
    {
        bool Sprint { get; }
    }
}