using Game.Source.Services;

namespace Game.Source.GameFSM
{
    public class MainScreenState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private IInputService _inputService;
        private MainScreen _mainScreen;


        public MainScreenState(GameStateMachine gameStateMachine, IGameFactory gameFactory, IInputService inputService)
        {
            _inputService = inputService;
            _gameFactory = gameFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _inputService.EnableUIInput();
            _mainScreen = _gameFactory.CreateScreen<MainScreen>();
            _mainScreen.Initialize(() => _gameStateMachine.SwitchState(GameFlow.StartLevel));
        }

        public void Exit()
        {
            _inputService.DisableUIInput();
            _mainScreen.Hide();
        }
    }
}