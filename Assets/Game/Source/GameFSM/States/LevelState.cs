using Game.Source.PlayerLogic;
using Game.Source.Services;
using Game.Source.Services.Factories;

namespace Game.Source.GameFSM.States
{
    public class LevelState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IInputService _inputService;
        private readonly IGameFactory _gameFactory;
        private readonly ILevelController _levelController;
        private PlayerMovement _playerMovement;

        public LevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory, ILevelController levelController,
            IInputService inputService)
        {
            _levelController = levelController;
            _gameFactory = gameFactory;
            _inputService = inputService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameFactory.Player.TogglePlayerLogic(true);

            _playerMovement = _gameFactory.Player.GetComponent<PlayerMovement>();
            _playerMovement.OnDestinationReached += _levelController.OnNewLocationReached;
            
            
            _levelController.NextLocation();
        }

        public void Exit()
        {
            _playerMovement.OnDestinationReached -= _levelController.OnNewLocationReached;

        }

    }
}