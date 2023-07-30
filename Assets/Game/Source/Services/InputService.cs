using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Source.Services
{
    public interface IInputService : IService
    {
        event Action OnFireInputDetected;
        Vector3 MousePosition { get; }
    }

    public class InputService : IInputService, IInitializableService, GameInput.IGameplayActions
    {
        public event Action OnFireInputDetected;
        public Vector3 MousePosition => 
            Application.isMobilePlatform ? Touchscreen.current.position.ReadValue() : Mouse.current.position.ReadValue();
        
        
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