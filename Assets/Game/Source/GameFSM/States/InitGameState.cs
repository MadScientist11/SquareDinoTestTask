using Game.Source.EnemyLogic;
using Game.Source.LevelLogic;
using Game.Source.Services;

namespace Game.Source.GameFSM
{
    public class InitGameState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly PlayerSpawnPoint _playerSpawnPoint;
        private readonly Level _level;

        public InitGameState(GameStateMachine gameStateMachine, IGameFactory gameFactory, Level level, PlayerSpawnPoint spawnPoint)
        {
            _level = level;
            _playerSpawnPoint = spawnPoint;
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameFactory.CreatePlayer(_playerSpawnPoint.Position, _playerSpawnPoint.Rotation);
            CreateEnemies();
        }

        public void Exit()
        {
            
        }

        private void CreateEnemies()
        {
            foreach (Location levelLocation in _level.Locations)
            {
                foreach (EnemySpawnPoint spawnPoint in levelLocation.EnemySpawnPoints)
                {
                    _gameFactory.CreateEnemy(spawnPoint.Position, spawnPoint.Rotation);
                }
            }
        }
    }
}