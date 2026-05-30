namespace Game.Core.UI
{
    public abstract class UIView<TViewModel> : UIView where TViewModel : IUIViewModel
    {
        protected TViewModel ViewModel { get; private set; }

        public void Initialize(TViewModel viewModel)
        {
            ViewModel = viewModel;
            base.Initialize();
            OnBind();
        }

        public override void Release()
        {
            base.Release();
            ViewModel = default;
        }

        protected abstract void OnBind();
    }
}
