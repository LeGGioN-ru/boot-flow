using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Core.StateMachine
{
    public sealed class StatesController<TEnum> : IStatesController<TEnum> where TEnum : struct, Enum
    {
        private readonly IStateFactory<TEnum> _factory;
        private IState _current;

        public StatesController(IStateFactory<TEnum> factory)
        {
            _factory = factory;
        }

        public async UniTask EnterStateAsync(TEnum code, CancellationToken ct)
        {
            if (_current != null)
                await _current.ExitAsync(ct);

            _current = _factory.Get(code);
            await _current.EnterAsync(ct);
        }
    }
}
