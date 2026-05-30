using System;
using System.Collections.Generic;

namespace Game.Core.Reactive
{
    public sealed class ReactiveValue<T>
    {
        private readonly List<Action<T>> _subscribers = new();
        private T _value;

        public ReactiveValue(T initialValue = default)
        {
            _value = initialValue;
        }

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(_value, value))
                    return;

                _value = value;

                var snapshot = _subscribers.ToArray();
                for (int i = 0; i < snapshot.Length; i++)
                    snapshot[i].Invoke(_value);
            }
        }

        public IDisposable Subscribe(Action<T> callback, bool invokeImmediately = true)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            _subscribers.Add(callback);

            if (invokeImmediately)
                callback.Invoke(_value);

            return new Subscription(this, callback);
        }

        private void Unsubscribe(Action<T> callback)
        {
            _subscribers.Remove(callback);
        }

        private sealed class Subscription : IDisposable
        {
            private ReactiveValue<T> _owner;
            private Action<T> _callback;

            public Subscription(ReactiveValue<T> owner, Action<T> callback)
            {
                _owner = owner;
                _callback = callback;
            }

            public void Dispose()
            {
                if (_owner == null)
                    return;

                _owner.Unsubscribe(_callback);
                _owner = null;
                _callback = null;
            }
        }
    }
}
