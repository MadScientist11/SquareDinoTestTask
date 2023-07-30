using Game.Source.LevelLogic;
using Game.Source.PlayerLogic;
using Game.Source.Services;

namespace Game.Source.GameFSM
{
    public class LevelState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IInputService _inputService;
        private IGameFactory _gameFactory;

        public LevelState(GameStateMachine gameStateMachine, IGameFactory gameFactory,
            IInputService inputService)
        {
            _gameFactory = gameFactory;
            _inputService = inputService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameFactory.Player.TogglePlayerLogic(true);
            _gameFactory.Player.GetComponent<PlayerMovement>().OnDestinationReached += EnableFireInput;

        }

        public void Exit()
        {
            _gameFactory.Player.GetComponent<PlayerMovement>().OnDestinationReached -= EnableFireInput;
        }

        private void EnableFireInput()
        {
            _inputService.EnablePlayerInput();
        }
    }
}