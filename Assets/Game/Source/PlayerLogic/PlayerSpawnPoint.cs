using UnityEngine;

namespace Game.Source.PlayerLogic
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public Mesh mesh;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(Position, 0.05f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawMesh(mesh, Position, Quaternion.LookRotation(transform.right), Vector3.one*8);
        }
    }
}