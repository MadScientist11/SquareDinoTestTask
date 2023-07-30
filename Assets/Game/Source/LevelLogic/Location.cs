using System.Collections.Generic;
using Game.Source.EnemyLogic;
using UnityEngine;

namespace Game.Source.LevelLogic
{
    [ExecuteInEditMode]
    public class Location : MonoBehaviour
    {
        [field: SerializeField] public List<EnemySpawnPoint> EnemySpawnPoints { get; private set; }
        [field: SerializeField] public WayPoint LocationWayPoint { get; private set; }

        private void OnValidate()
        {
            LocationWayPoint = transform.GetComponentInChildren<WayPoint>();
            
            EnemySpawnPoints.Clear();
            foreach (EnemySpawnPoint spawnPoint in transform.GetComponentsInChildren<EnemySpawnPoint>())
            {
                EnemySpawnPoints.Add(spawnPoint);
            }
        }
    }
}
