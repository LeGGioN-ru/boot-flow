using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Core.StateMachine
{
    public interface IStatesController<TEnum> where TEnum : struct, Enum
    {
        UniTask EnterStateAsync(TEnum code, CancellationToken ct);
    }
}
