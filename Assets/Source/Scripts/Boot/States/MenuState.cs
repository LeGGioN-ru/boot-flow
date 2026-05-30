using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.StateMachine;
using Game.UI.Menu;

namespace Game.Boot.States
{
    public sealed class MenuState : IState
    {
        private readonly MenuUIView _view;
        private readonly IStatesController<BootState> _controller;

        public MenuState(MenuUIView view, IStatesController<BootState> controller)
        {
            _view = view;
            _controller = controller;
        }

        public UniTask EnterAsync(CancellationToken ct)
        {
            _view.Initialize(new MenuUIViewModel(() => _controller.EnterStateAsync(BootState.Load, ct).Forget()));
            return UniTask.CompletedTask;
        }

        public UniTask ExitAsync(CancellationToken ct)
        {
            _view.Release();
            return UniTask.CompletedTask;
        }
    }
}
