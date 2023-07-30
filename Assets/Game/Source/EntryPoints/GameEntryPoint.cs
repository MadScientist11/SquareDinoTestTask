using Game.Source.GameFSM;
using UnityEngine;
using VContainer;

namespace Game.Source.EntryPoints
{
    public class GameEntryPoint : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start() => 
            _gameStateMachine.SwitchState(GameFlow.InitGame);
    }
}
