using System.Collections.Generic;
using Game.Source.PlayerLogic;
using UnityEngine;

namespace Game.Source.GameFSM
{
    public class GameStateMachine
    {
        public Player Player { get; set; }
        
        private readonly Dictionary<GameFlow, IGameState> _states;
        private readonly StatesFactory _statesFactory;
        private IGameState _currentState;

        public GameStateMachine(StatesFactory statesFactory)
        {
            _statesFactory = statesFactory;
            _states = new Dictionary<GameFlow, IGameState>();
        }


        public void SwitchState(GameFlow nextState)
        {

            if (!_states.TryGetValue(nextState, out var _))
            {
                _states.Add(nextState, _statesFactory.CreateState(nextState));
            }
            
            if (_states[nextState] == _currentState)
            {
                Debug.LogWarning($"State {nextState} is already active");
                return;
            }

            _currentState?.Exit();
            _currentState = _states[nextState];
            _currentState.Enter();
        }
    }
}