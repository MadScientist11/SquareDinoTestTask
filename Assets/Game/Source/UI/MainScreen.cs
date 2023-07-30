using System;
using VContainer;

namespace Game.Source.Services
{
    public class MainScreen : BaseScreen
    {
        private IInputService _inputService;
        private Action _onStartGame;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Initialize(Action onStartGame)
        {
            _onStartGame = onStartGame;
        }

        private void OnEnable()
        {
            _inputService.OnUILeftClicked += StartGame;
        }

        private void OnDisable()
        {
            _inputService.OnUILeftClicked -= StartGame;
        }

        private void StartGame()
        {
            _onStartGame?.Invoke();
        }
    }
}