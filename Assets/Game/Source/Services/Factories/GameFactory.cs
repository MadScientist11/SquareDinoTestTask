using System;
using System.Collections.Generic;
using Game.Source.EnemyLogic;
using Game.Source.PlayerLogic;
using Game.Source.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Source.Services.Factories
{
    public interface IGameFactory
    {
        Player Player { get; }
        Player CreatePlayer(Vector3 position, Quaternion rotation);
        Enemy CreateEnemy(Vector3 spawnPointPosition, Quaternion spawnPointRotation);
        T CreateScreen<T>() where T : BaseScreen;
        
        T InstancePrefab<T>(string path) where T : MonoBehaviour;
        T InstancePrefab<T>(string path, Transform parent, Vector3 position) where T : Object;
        T InstancePrefabInjected<T>(string path) where T : MonoBehaviour;
        T InstancePrefabInjected<T>(string path, Vector3 position) where T : MonoBehaviour;
        T InstancePrefabInjected<T>(string path, Vector3 position, Quaternion rotation) where T : MonoBehaviour;
        T InstancePrefabInjected<T>(string path, Transform parent) where T : MonoBehaviour;
    }


    public class GameFactory : IGameFactory
    {
        private readonly IObjectResolver _instantiator;
        private readonly IAssetProvider _assetProvider;
        private UiRoot _uiRoot;

        private const string UiRootPath = "MainCanvas";

        private readonly Dictionary<Type, string> _screenPaths = new()
        {
            { typeof(MainScreen), GameConstants.Assets.MainScreenPath },
        };

        public Player Player { get; private set; }


        public GameFactory(IObjectResolver instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public Player CreatePlayer(Vector3 position, Quaternion rotation)
        {
            Player = InstancePrefabInjected<Player>(GameConstants.Assets.PlayerPath, position, rotation);
            return Player;
        }

        public Enemy CreateEnemy(Vector3 position, Quaternion rotation)
        {
            return InstancePrefabInjected<Enemy>(GameConstants.Assets.EnemyPath, position, rotation);
        }

        public T CreateScreen<T>() where T : BaseScreen
        {
            GetOrCreateUIRoot();
            return InstancePrefabInjected<T>(_screenPaths[typeof(T)], _uiRoot.transform);
        }


        private UiRoot GetOrCreateUIRoot()
        {
            if (_uiRoot == null)
                _uiRoot = InstancePrefab<UiRoot>(GameConstants.Assets.UiRootPath);

            return _uiRoot;
        }


        public T InstancePrefab<T>(string path) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            return Object.Instantiate(asset);
        }

        public T InstancePrefab<T>(string path, Transform parent, Vector3 position) where T : Object
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            return Object.Instantiate(asset, position, Quaternion.identity, parent);
        }

        public T InstancePrefabInjected<T>(string path) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public T InstancePrefabInjected<T>(string path, Vector3 position) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, position, Quaternion.identity);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public T InstancePrefabInjected<T>(string path, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, position, rotation);
            instance.gameObject.SetActive(true);
            return instance;
        }

        public T InstancePrefabInjected<T>(string path, Transform parent) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, parent);
            instance.gameObject.SetActive(true);
            return instance;
        }
    }
}