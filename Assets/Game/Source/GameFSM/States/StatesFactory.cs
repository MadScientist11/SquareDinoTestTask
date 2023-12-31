using System;
using VContainer;

namespace Game.Source.GameFSM.States
{
    public class StatesFactory
    {
        private IObjectResolver _instantiator;

        public StatesFactory(IObjectResolver instantiator)
        {
            _instantiator = instantiator;
        }

        public IGameState CreateState(GameFlow gameState)
        {
            return gameState switch
            {
                GameFlow.InitGame => _instantiator.Resolve<InitGameState>(),
                GameFlow.MainScreenState => _instantiator.Resolve<MainScreenState>(),
                GameFlow.StartLevel => _instantiator.Resolve<LevelState>(),
                GameFlow.CompleteLevel => _instantiator.Resolve<CompleteLevelState>(),
                _ => throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null)
            };
        }
    }
}