using Game.Source.PlayerLogic;
using Game.Source.Services;

namespace Game.Source.GameFSM
{
    public class InitGameState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly PlayerSpawnPoint _playerSpawnPoint;
        private readonly ILevelController _levelController;

        public InitGameState(GameStateMachine gameStateMachine, IGameFactory gameFactory,
            ILevelController levelController, PlayerSpawnPoint spawnPoint)
        {
            _levelController = levelController;
            _playerSpawnPoint = spawnPoint;
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            InitializePlayer();
            _levelController.InitializeLevel();
            _gameStateMachine.SwitchState(GameFlow.MainScreenState);
        }

        public void Exit()
        {
        }

        private void InitializePlayer()
        {
            Player player = _gameFactory.CreatePlayer(_playerSpawnPoint.Position, _playerSpawnPoint.Rotation);
            player.TogglePlayerLogic(false);
        }
    }
}