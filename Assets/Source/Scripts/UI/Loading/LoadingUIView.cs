using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Loading
{
    public sealed class LoadingUIView : UIView<LoadingUIViewModel>
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private float _fillSpeed = 3f;

        private CancellationTokenSource _animationCts;
        private float _target;

        protected override void OnBind()
        {
            _target = 0f;
            _progressBar.fillAmount = 0f;

            _animationCts = new CancellationTokenSource();
            AddSubscription(ViewModel.Progress.Subscribe(value => _target = value));
            FollowTargetAsync(_animationCts.Token).Forget();
        }

        public override void Release()
        {
            if (_animationCts != null)
            {
                _animationCts.Cancel();
                _animationCts.Dispose();
                _animationCts = null;
            }

            base.Release();
        }

        private async UniTaskVoid FollowTargetAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _target, _fillSpeed * Time.deltaTime);
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }
        }
    }
}
