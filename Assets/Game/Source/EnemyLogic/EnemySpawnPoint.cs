using System;
using UnityEngine;

namespace Game.Source.EnemyLogic
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Position, 0.1f);
        }
    }
}
