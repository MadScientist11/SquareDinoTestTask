using Game.Source.Services;

namespace Game.Source.GameFSM
{
    public class MainScreenState : IGameState
    {
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;


        public MainScreenState(GameStateMachine gameStateMachine, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}