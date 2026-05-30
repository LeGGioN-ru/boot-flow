using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Services;
using Game.Core.StateMachine;

namespace Game.Boot
{
    public sealed class BootFlowService : Service
    {
        private readonly IStatesController<BootState> _controller;

        public BootFlowService(IStatesController<BootState> controller)
        {
            _controller = controller;
        }

        protected override UniTask OnInitializeAsync(CancellationToken ct)
        {
            _controller.EnterStateAsync(BootState.Splash, ct).Forget();
            return UniTask.CompletedTask;
        }
    }
}
