using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.StateMachine;
using Game.UI.Splash;

namespace Game.Boot.States
{
    public sealed class SplashState : IState
    {
        private readonly SplashUIView _view;
        private readonly IStatesController<BootState> _controller;
        private readonly BootSettings _settings;

        public SplashState(SplashUIView view, IStatesController<BootState> controller, BootSettings settings)
        {
            _view = view;
            _controller = controller;
            _settings = settings;
        }

        public async UniTask EnterAsync(CancellationToken ct)
        {
            _view.Initialize();
            await UniTask.Delay(TimeSpan.FromSeconds(_settings.SplashDelaySeconds), cancellationToken: ct);
            _controller.EnterStateAsync(BootState.Load, ct).Forget();
        }

        public UniTask ExitAsync(CancellationToken ct)
        {
            _view.Release();
            return UniTask.CompletedTask;
        }
    }
}
