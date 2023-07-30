using Game.Source.LevelLogic;
using Game.Source.Services;

namespace Game.Source.GameFSM
{
    public class LevelState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly Level _level;
        private readonly IInputService _inputService;

        public LevelState(GameStateMachine gameStateMachine,  Level level,
            IInputService inputService)
        {
            _inputService = inputService;
            _level = level;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _inputService.EnablePlayerInput();
            _gameStateMachine.Player.TogglePlayerLogic(true);
        }

        public void Exit()
        {
        }
    }
}