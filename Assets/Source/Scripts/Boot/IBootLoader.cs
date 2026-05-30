using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Reactive;

namespace Game.Boot
{
    public interface IBootLoader
    {
        ReactiveValue<float> Progress { get; }
        UniTask LoadAsync(CancellationToken ct);
    }
}
