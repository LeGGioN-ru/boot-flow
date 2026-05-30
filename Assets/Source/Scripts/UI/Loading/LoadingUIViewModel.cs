using Game.Core.Reactive;
using Game.Core.UI;

namespace Game.UI.Loading
{
    public sealed class LoadingUIViewModel : IUIViewModel
    {
        public ReactiveValue<float> Progress { get; }

        public LoadingUIViewModel(ReactiveValue<float> progress)
        {
            Progress = progress;
        }
    }
}
