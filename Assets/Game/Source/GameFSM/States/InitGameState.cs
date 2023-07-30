using Game.Source.Services;

namespace Game.Source.GameFSM
{
    public class InitGameState : IGameState
    {
        private GameStateMachine _gameStateMachine;
        private IGameFactory _gameFactory;
        private PlayerSpawnPoint _playerSpawnPoint;

        public InitGameState(GameStateMachine gameStateMachine, IGameFactory gameFactory, PlayerSpawnPoint spawnPoint)
        {
            _playerSpawnPoint = spawnPoint;
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameFactory.CreatePlayer(_playerSpawnPoint.Position, _playerSpawnPoint.Rotation);

        }

        public void Exit()
        {
            
        }
    }
}