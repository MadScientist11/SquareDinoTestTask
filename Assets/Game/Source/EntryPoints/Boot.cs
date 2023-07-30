using System.Collections.Generic;
using BattleCity.Source;
using Game.Source.Scopes;
using Game.Source.Services;
using UnityEngine;
using VContainer;

namespace Game.Source.EntryPoints
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private AppLifetimeScope _appLifetimeScope;
        
        private IReadOnlyList<IInitializableService> _services;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(IReadOnlyList<IInitializableService> allServices, ISceneLoader sceneLoader)
        {
            _services = allServices;
            _sceneLoader = sceneLoader;
        }

        private void Awake() => 
            DontDestroyOnLoad(_appLifetimeScope);

        private void Start()
        {
            InitializeServices(_services);
            _sceneLoader.LoadScene(GameConstants.Scenes.GamePath);
        }
    
        private void InitializeServices(IReadOnlyList<IInitializableService> services)
        {
            foreach (IInitializableService service in services)
            {
                service.Initialize();
            }
        }
    }
}
