using System;
using System.Collections.Generic;
using Game.Boot;
using Game.Boot.States;
using Game.Core.Services;
using Game.Core.StateMachine;
using Game.Infrastructure;
using Game.UI.Loading;
using Game.UI.Menu;
using Game.UI.Splash;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootLifetimeScope : LifetimeScope
{
    [SerializeField] private SplashUIView _splashView;
    [SerializeField] private LoadingUIView _loadingView;
    [SerializeField] private MenuUIView _menuView;
    [SerializeField] private BootSettings _bootSettings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_bootSettings);

        builder.RegisterComponent(_splashView);
        builder.RegisterComponent(_loadingView);
        builder.RegisterComponent(_menuView);

        builder.Register<SplashState>(Lifetime.Transient);
        builder.Register<LoadState>(Lifetime.Transient);
        builder.Register<MenuState>(Lifetime.Transient);

        IReadOnlyDictionary<BootState, Type> statesMap = new Dictionary<BootState, Type>
        {
            [BootState.Splash] = typeof(SplashState),
            [BootState.Load] = typeof(LoadState),
            [BootState.Menu] = typeof(MenuState),
        };
        builder.RegisterInstance(statesMap);

        builder.Register<StateFactory<BootState>>(Lifetime.Singleton).As<IStateFactory<BootState>>();
        builder.Register<StatesController<BootState>>(Lifetime.Singleton).As<IStatesController<BootState>>();

        builder.Register<BootLoader>(Lifetime.Singleton).As<IBootLoader>();
        builder.Register<BootFlowService>(Lifetime.Singleton).As<IService>();

        builder.RegisterEntryPoint<ServicesEntryPoint>();
    }
}