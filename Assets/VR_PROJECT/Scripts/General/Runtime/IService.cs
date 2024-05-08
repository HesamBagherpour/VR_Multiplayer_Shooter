using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace VR_PROJECT.General
{
    public interface IService<T>
    {
        UniTask<Result<T>> Init(); 
    }
}