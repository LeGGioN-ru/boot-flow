using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Services;
using VContainer.Unity;

namespace Game.Infrastructure
{
    public sealed class ServicesEntryPoint : IStartable, IDisposable
    {
        private readonly IReadOnlyList<IService> _services;
        private readonly CancellationTokenSource _cts = new();

        public ServicesEntryPoint(IReadOnlyList<IService> services)
        {
            _services = services;
        }

        public void Start()
        {
            InitializeAsync(_cts.Token).Forget();
        }

        public void Dispose()
        {
            _cts.Cancel();

            for (int i = 0; i < _services.Count; i++)
                _services[i].ReleaseAsync(CancellationToken.None).Forget();

            _cts.Dispose();
        }

        private async UniTaskVoid InitializeAsync(CancellationToken ct)
        {
            for (int i = 0; i < _services.Count; i++)
                await _services[i].InitializeAsync(ct);
        }
    }
}
