using System.Collections.Generic;
using Game.Source.EnemyLogic;
using Game.Source.Services;
using Game.Source.Services.Factories;
using UnityEngine;
using VContainer;

namespace Game.Source.LevelSystem
{
    public class Location : MonoBehaviour
    {
        [field: SerializeField] public List<EnemySpawnPoint> EnemySpawnPoints { get; private set; }
        [field: SerializeField] public WayPoint LocationWayPoint { get; private set; }
        public List<Enemy> LocationEnemies { get; private set; }
        
        public int ActiveEnemiesCount { get; private set; }

        private IGameFactory _gameFactory;
        private ILevelController _levelController;

        [Inject]
        public void Construct(IGameFactory gameFactory, ILevelController levelController)
        {
            _levelController = levelController;
            _gameFactory = gameFactory;
        }

        private void OnValidate()
        {
            LocationWayPoint = transform.GetComponentInChildren<WayPoint>();
            
            EnemySpawnPoints.Clear();
            foreach (EnemySpawnPoint spawnPoint in transform.GetComponentsInChildren<EnemySpawnPoint>())
            {
                EnemySpawnPoints.Add(spawnPoint);
            }
        }

        public void PopulateLocationWithEnemies()
        {
            LocationEnemies = new();
            foreach (EnemySpawnPoint enemySpawnPoint in EnemySpawnPoints)
            {
                Enemy enemy = _gameFactory.CreateEnemy(enemySpawnPoint.Position, enemySpawnPoint.Rotation);
                LocationEnemies.Add(enemy);
            }

            ActiveEnemiesCount = LocationEnemies.Count;
        }

        public void SetEnemyNeutralized(Enemy enemy)
        {
            ActiveEnemiesCount--;
            LocationEnemies.Remove(enemy);

            if (LocationEnemies.Count == 0) 
                _levelController.NextLocation();
        }
    }
}
