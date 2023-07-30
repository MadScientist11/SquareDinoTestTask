using System;
using System.Collections.Generic;
using BattleCity.Source;
using Game.Source.EnemyLogic;
using Game.Source.PlayerLogic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Source.Services
{
    public interface IGameFactory
    {
        Player CreatePlayer(Vector3 position, Quaternion rotation);
        Enemy CreateEnemy(Vector3 spawnPointPosition, Quaternion spawnPointRotation);
        T CreateScreen<T>() where T : BaseScreen;
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


        public GameFactory(IObjectResolver instantiator, IAssetProvider assetProvider)
        {
            _instantiator = instantiator;
            _assetProvider = assetProvider;
        }

        public Player CreatePlayer(Vector3 position, Quaternion rotation)
        {
            return InstancePrefabInjected<Player>(GameConstants.Assets.PlayerPath, position, rotation);
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



        private T InstancePrefab<T>(string path) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            return Object.Instantiate(asset);
        }

        private T InstancePrefab<T>(string path, Transform parent, Vector3 position) where T : Object
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            return Object.Instantiate(asset, position, Quaternion.identity, parent);
        }

        private T InstancePrefabInjected<T>(string path) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset);
            instance.gameObject.SetActive(true);
            return instance;
        }

        private T InstancePrefabInjected<T>(string path, Vector3 position) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, position, Quaternion.identity);
            instance.gameObject.SetActive(true);
            return instance;
        }

        private T InstancePrefabInjected<T>(string path, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, position, rotation);
            instance.gameObject.SetActive(true);
            return instance;
        }

        private T InstancePrefabInjected<T>(string path, Transform parent) where T : MonoBehaviour
        {
            T asset = _assetProvider.LoadAsset<T>(path);
            asset.gameObject.SetActive(false);
            T instance = _instantiator.Instantiate(asset, parent);
            instance.gameObject.SetActive(true);
            return instance;
        }
    }
}