using System;
using VContainer;

namespace Game.Source.GameFSM
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
                GameFlow.MainScreenState => null,
                GameFlow.StartLevel => null,
                GameFlow.CompleteLevel => null,
                _ => throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null)
            };
        }
    }
}