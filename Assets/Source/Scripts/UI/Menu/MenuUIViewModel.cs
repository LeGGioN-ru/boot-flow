using System;
using Game.Core.UI;

namespace Game.UI.Menu
{
    public sealed class MenuUIViewModel : IUIViewModel
    {
        private readonly Action _onRestart;

        public MenuUIViewModel(Action onRestart)
        {
            _onRestart = onRestart;
        }

        public void Restart()
        {
            _onRestart?.Invoke();
        }
    }
}
