using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core.UI
{
    public abstract class UIView : MonoBehaviour
    {
        private readonly List<IDisposable> _subscriptions = new();

        public virtual void Initialize()
        {
            gameObject.SetActive(true);
        }

        public virtual void Release()
        {
            for (int i = 0; i < _subscriptions.Count; i++)
                _subscriptions[i].Dispose();

            _subscriptions.Clear();
            gameObject.SetActive(false);
        }

        protected void AddSubscription(IDisposable subscription)
        {
            _subscriptions.Add(subscription);
        }
    }
}
