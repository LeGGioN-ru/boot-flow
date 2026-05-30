using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Core.StateMachine
{
    public interface IState
    {
        UniTask EnterAsync(CancellationToken ct);
        UniTask ExitAsync(CancellationToken ct);
    }
}
