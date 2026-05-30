using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.StateMachine;
using Game.UI.Loading;

namespace Game.Boot.States
{
    public sealed class LoadState : IState
    {
        private readonly LoadingUIView _view;
        private readonly IStatesController<BootState> _controller;
        private readonly IBootLoader _loader;

        public LoadState(LoadingUIView view, IStatesController<BootState> controller, IBootLoader loader)
        {
            _view = view;
            _controller = controller;
            _loader = loader;
        }

        public async UniTask EnterAsync(CancellationToken ct)
        {
            _view.Initialize(new LoadingUIViewModel(_loader.Progress));
            await _loader.LoadAsync(ct);
            _controller.EnterStateAsync(BootState.Menu, ct).Forget();
        }

        public UniTask ExitAsync(CancellationToken ct)
        {
            _view.Release();
            return UniTask.CompletedTask;
        }
    }
}
