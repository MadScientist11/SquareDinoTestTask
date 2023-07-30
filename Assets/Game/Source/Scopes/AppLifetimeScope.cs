using Game.Source.Services;
using VContainer;
using VContainer.Unity;

namespace Game.Source.Scopes
{
    public class AppLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ProjectileFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AssetProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
