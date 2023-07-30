using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Source.Services
{
    public interface IInputService : IService
    {
        event Action OnFireInputDetected;
        event Action OnUILeftClicked;
        Vector3 MousePosition { get; }
        void EnablePlayerInput();
        void DisablePlayerInput();
        void EnableUIInput();
        void DisableUIInput();
    }

    public class InputService : IInputService, IInitializableService, GameInput.IGameplayActions, GameInput.IUIActions
    {
        public event Action OnFireInputDetected;
        public event Action OnUILeftClicked;
        public Vector3 MousePosition => 
            Application.isMobilePlatform ? Touchscreen.current.position.ReadValue() : Mouse.current.position.ReadValue();
        
        
        private GameInput _input;

        public void Initialize()
        {
            _input = new GameInput();
            _input.UI.SetCallbacks(this);
            _input.Gameplay.SetCallbacks(this);
        }

        public void EnablePlayerInput() => 
            _input.Gameplay.Enable();

        public void DisablePlayerInput() => 
            _input.Gameplay.Disable();
        public void EnableUIInput() => 
            _input.UI.Enable();
        public void DisableUIInput() => 
            _input.UI.Disable();
        


        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                OnFireInputDetected?.Invoke();
        }

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
                OnUILeftClicked?.Invoke();
        }
    }

 
}