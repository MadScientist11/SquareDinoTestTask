using UnityEngine;

namespace Game.Source.EnemyLogic
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        [SerializeField] private Mesh _enemyMesh;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Position, 0.1f);
            
            if (Application.isPlaying)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawMesh(_enemyMesh, Position, Quaternion.LookRotation(transform.right), Vector3.one*8);
        }
    }
}
