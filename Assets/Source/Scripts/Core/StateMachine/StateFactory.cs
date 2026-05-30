using System;
using System.Collections.Generic;
using VContainer;

namespace Game.Core.StateMachine
{
    public sealed class StateFactory<TEnum> : IStateFactory<TEnum> where TEnum : struct, Enum
    {
        private readonly IObjectResolver _resolver;
        private readonly IReadOnlyDictionary<TEnum, Type> _map;

        public StateFactory(IObjectResolver resolver, IReadOnlyDictionary<TEnum, Type> map)
        {
            _resolver = resolver;
            _map = map;
        }

        public IState Get(TEnum code)
        {
            if (!_map.TryGetValue(code, out var type))
                throw new InvalidOperationException($"State for code '{code}' is not registered.");

            return (IState)_resolver.Resolve(type);
        }
    }
}
