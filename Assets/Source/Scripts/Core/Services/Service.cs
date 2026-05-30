using System.Threading;
using Cysharp.Threading.Tasks;

namespace Game.Core.Services
{
    public abstract class Service : IService
    {
        private CancellationTokenSource _cts;

        protected CancellationToken ServiceToken => _cts?.Token ?? CancellationToken.None;

        public async UniTask InitializeAsync(CancellationToken ct)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            await OnInitializeAsync(_cts.Token);
        }

        public async UniTask ReleaseAsync(CancellationToken ct)
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            await OnReleaseAsync(ct);
        }

        protected virtual UniTask OnInitializeAsync(CancellationToken ct) => UniTask.CompletedTask;

        protected virtual UniTask OnReleaseAsync(CancellationToken ct) => UniTask.CompletedTask;
    }
}
