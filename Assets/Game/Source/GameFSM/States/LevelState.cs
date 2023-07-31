using Game.Source.PlayerLogic;
using Game.Source.Services;

namespace Game.Source.GameFSM
{
    public class LevelState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IInputService _inputService;
        private readonly IGameFactory _gameFactory;
        private readonly ILevelController _levelController;

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
            PlayerMovement playerMovement = _gameFactory.Player.GetComponent<PlayerMovement>();
            playerMovement.SetDestination(_levelController.CurrentLocation.LocationWayPoint.Position);
        }

        public void Exit()
        {
          
        }

    }
}