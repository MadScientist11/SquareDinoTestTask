using Game.Source.GameFSM;
using Game.Source.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Source.Scopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
        [SerializeField] private WayPoints _wayPoints;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            
            
            builder.Register<GameStateMachine>(Lifetime.Singleton).AsSelf();
            builder.Register<StatesFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<InitGameState>(Lifetime.Singleton).AsSelf();
            
            builder.RegisterComponent(_playerSpawnPoint);
            builder.RegisterComponent(_wayPoints);
        }
    }
}
