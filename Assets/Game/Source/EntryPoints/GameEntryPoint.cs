using BattleCity.Source;
using Game.Source.GameFSM;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void Awake()
        {
            if (_gameStateMachine == null)
            {
                Debug.LogWarning("Couldn't inject state machine, loading a boot scene...");
                SceneManager.LoadScene(GameConstants.Scenes.BootPath);
            }
        }

        private void Start() => 
            _gameStateMachine.SwitchState(GameFlow.InitGame);
    }
}
