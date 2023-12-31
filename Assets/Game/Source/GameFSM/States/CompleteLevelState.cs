﻿using Game.Source.Services;

namespace Game.Source.GameFSM.States
{
    public class CompleteLevelState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public CompleteLevelState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            _sceneLoader.LoadScene(GameConstants.Scenes.GamePath);
        }

        public void Exit()
        {
        }
    }
}