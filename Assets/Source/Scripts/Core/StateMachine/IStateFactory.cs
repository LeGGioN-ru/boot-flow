using System;

namespace Game.Core.StateMachine
{
    public interface IStateFactory<TEnum> where TEnum : struct, Enum
    {
        IState Get(TEnum code);
    }
}
