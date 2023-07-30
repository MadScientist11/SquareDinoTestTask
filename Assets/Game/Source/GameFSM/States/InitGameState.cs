using Game.Source.EnemyLogic;
using Game.Source.LevelLogic;
using Game.Source.PlayerLogic;
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
            InitPlayer();
            CreateEnemies();
            _gameStateMachine.SwitchState(GameFlow.MainScreenState);
        }

        public void Exit()
        {
            
        }

        private void InitPlayer()
        {
            Player player = _gameFactory.CreatePlayer(_playerSpawnPoint.Position, _playerSpawnPoint.Rotation);
            player.TogglePlayerLogic(false);
            _gameStateMachine.Player = player;
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