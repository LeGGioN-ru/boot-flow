using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.Reactive;

namespace Game.Boot
{
    public sealed class BootLoader : IBootLoader
    {
        private readonly BootSettings _settings;

        public ReactiveValue<float> Progress { get; } = new(0f);

        public BootLoader(BootSettings settings)
        {
            _settings = settings;
        }

        public async UniTask LoadAsync(CancellationToken ct)
        {
            Progress.Value = 0f;

            int steps = _settings.LoadStepsCount;
            for (int step = 0; step < steps; step++)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_settings.LoadStepDelaySeconds), cancellationToken: ct);
                Progress.Value = (float)(step + 1) / steps;
            }
        }
    }
}
