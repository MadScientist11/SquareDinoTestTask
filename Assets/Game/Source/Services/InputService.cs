using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Source.Services
{
    public interface IInputService : IService
    {
        event Action OnFireInputDetected;
    }

    public class InputService : IInputService, IInitializableService, GameInput.IGameplayActions
    {
        public event Action OnFireInputDetected;
        
        private GameInput _input;

        public void Initialize()
        {
            _input = new GameInput();
            _input.Gameplay.SetCallbacks(this);
            _input.Gameplay.Enable();
        }

        public void DisablePlayerInput()
        {
            _input.Gameplay.Disable();
        }


        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                OnFireInputDetected?.Invoke();
        }
    }

 
}