using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Core.Services
{
    public interface IService
    {
        UniTask InitializeAsync(CancellationToken ct);
        UniTask ReleaseAsync(CancellationToken ct);
    }
}
