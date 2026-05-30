using Game.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Menu
{
    public sealed class MenuUIView : UIView<MenuUIViewModel>
    {
        [SerializeField] private Button _restartButton;

        protected override void OnBind()
        {
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        public override void Release()
        {
            _restartButton.onClick.RemoveListener(OnRestartClicked);
            base.Release();
        }

        private void OnRestartClicked()
        {
            ViewModel.Restart();
        }
    }
}
